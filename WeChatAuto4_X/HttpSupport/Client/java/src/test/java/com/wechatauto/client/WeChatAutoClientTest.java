package com.wechatauto.client;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotNull;
import static org.junit.jupiter.api.Assertions.assertNull;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.Assertions.assertTrue;

import java.io.IOException;
import java.io.OutputStream;
import java.net.InetSocketAddress;
import java.nio.charset.StandardCharsets;
import java.util.Arrays;
import java.util.concurrent.ConcurrentLinkedQueue;

import org.junit.jupiter.api.AfterAll;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.sun.net.httpserver.HttpExchange;
import com.sun.net.httpserver.HttpServer;
import com.wechatauto.client.models.AddFriendsOptions;
import com.wechatauto.client.models.AddFriendsRequest;
import com.wechatauto.client.models.NavigationType;
import com.wechatauto.client.models.SendMessageRequest;
import com.wechatauto.client.models.SwitchNavigationRequest;

/**
 * Unit tests using a JDK built-in {@link HttpServer} as a fake backend, so no
 * live WeChat automation server is required.
 */
public class WeChatAutoClientTest {

    private static final ObjectMapper MAPPER = new ObjectMapper();

    private static HttpServer server;
    private static int port;
    private static final ConcurrentLinkedQueue<CapturedRequest> captured = new ConcurrentLinkedQueue<>();

    static class CapturedRequest {
        String method;
        String path;     // decoded path
        String rawPath;  // encoded path
        String query;    // raw query (null when absent)
        String apiKey;
        String contentType;
        String body;
    }

    @BeforeAll
    static void startServer() throws IOException {
        server = HttpServer.create(new InetSocketAddress("127.0.0.1", 0), 0);
        server.createContext("/", WeChatAutoClientTest::handle);
        server.start();
        port = server.getAddress().getPort();
    }

    @AfterAll
    static void stopServer() {
        if (server != null) {
            server.stop(0);
        }
    }

    @BeforeEach
    void clearCaptured() {
        captured.clear();
    }

    static void handle(HttpExchange exchange) throws IOException {
        CapturedRequest c = new CapturedRequest();
        c.method = exchange.getRequestMethod();
        c.path = exchange.getRequestURI().getPath();
        c.rawPath = exchange.getRequestURI().getRawPath();
        c.query = exchange.getRequestURI().getRawQuery();
        c.apiKey = exchange.getRequestHeaders().getFirst(WeChatAutoClient.API_KEY_HEADER);
        c.contentType = exchange.getRequestHeaders().getFirst("Content-Type");
        c.body = new String(exchange.getRequestBody().readAllBytes(), StandardCharsets.UTF_8);
        captured.add(c);

        byte[] resp = "{\"ok\":true}".getBytes(StandardCharsets.UTF_8);
        exchange.getResponseHeaders().set("Content-Type", "application/json");
        exchange.sendResponseHeaders(200, resp.length);
        try (OutputStream os = exchange.getResponseBody()) {
            os.write(resp);
        }
    }

    private static WeChatAutoClient client() {
        return new WeChatAutoClient("http://127.0.0.1:" + port, "secret-key");
    }

    private static CapturedRequest last() {
        CapturedRequest c = captured.poll();
        assertNotNull(c, "expected a captured request but none was made");
        return c;
    }

    @Test
    void getClientsSendsGetWithApiKey() {
        String response = client().getClients();

        assertEquals("{\"ok\":true}", response);
        CapturedRequest c = last();
        assertEquals("GET", c.method);
        assertEquals("/api/v1/clients", c.path);
        assertNull(c.query);
        assertEquals("secret-key", c.apiKey);
    }

    @Test
    void urlEncodesPathSegments() {
        client().getFriendCache("c1", "a b");

        CapturedRequest c = last();
        assertEquals("GET", c.method);
        assertEquals("/api/v1/clients/c1/friends/cache/a b", c.path);
        assertEquals("/api/v1/clients/c1/friends/cache/a%20b", c.rawPath);
    }

    @Test
    void sendMessagePostsSnakeCaseJson() throws Exception {
        SendMessageRequest req = new SendMessageRequest();
        req.setWho("张三");
        req.setMessage("hello");
        req.setAtUsers(Arrays.asList("a", "b"));

        client().sendMessage("client-1", req);

        CapturedRequest c = last();
        assertEquals("POST", c.method);
        assertEquals("/api/v1/clients/client-1/messages/send", c.path);
        assertEquals("application/json", c.contentType);
        assertEquals("secret-key", c.apiKey);

        JsonNode node = MAPPER.readTree(c.body);
        assertEquals("张三", node.get("who").asText());
        assertEquals("hello", node.get("message").asText());
        assertEquals("a", node.get("at_users").get(0).asText());
        assertEquals("b", node.get("at_users").get(1).asText());
    }

    @Test
    void addFriendsSerializesNestedOptions() throws Exception {
        AddFriendsRequest req = new AddFriendsRequest();
        req.setFriends(Arrays.asList("13800000000"));
        AddFriendsOptions opts = new AddFriendsOptions();
        opts.setIntervalTime(5);
        opts.setIsCloseWin(false);
        opts.setSayHi("hi");
        req.setOptions(opts);

        client().addFriends("c1", req);

        CapturedRequest c = last();
        assertEquals("POST", c.method);
        assertEquals("/api/v1/clients/c1/friends/add", c.path);

        JsonNode node = MAPPER.readTree(c.body);
        assertEquals("13800000000", node.get("friends").get(0).asText());
        JsonNode options = node.get("options");
        assertEquals(5, options.get("interval_time").asInt());
        assertEquals(false, options.get("is_close_win").asBoolean());
        assertEquals("hi", options.get("say_hi").asText());
    }

    @Test
    void switchNavigationSerializesEnumAsExactString() throws Exception {
        SwitchNavigationRequest req = new SwitchNavigationRequest();
        req.setNavigationType(NavigationType.MOMENTS);

        client().switchNavigation("c1", req);

        CapturedRequest c = last();
        assertEquals("POST", c.method);
        assertEquals("/api/v1/clients/c1/navigation/switch", c.path);
        JsonNode node = MAPPER.readTree(c.body);
        assertEquals("朋友圈", node.get("navigation_type").asText());
    }

    @Test
    void getFriendsAppliesFromCacheQueryParam() {
        client().getFriends("c1", false);

        CapturedRequest c = last();
        assertEquals("GET", c.method);
        assertEquals("/api/v1/clients/c1/friends", c.path);
        assertEquals("fromCache=false", c.query);
    }

    @Test
    void getMessageHistoryBuildsQueryParams() {
        client().getMessageHistory("c1", "alice", null, "2024-01-01", "2024-02-01");

        CapturedRequest c = last();
        assertEquals("GET", c.method);
        assertEquals("/api/v1/clients/c1/messages/history", c.path);
        assertEquals("who=alice&start_date=2024-01-01&end_date=2024-02-01", c.query);
    }

    @Test
    void exitGroupSendsDeleteWithQueryAndEncodedPath() {
        client().exitGroup("c1", "test group", false);

        CapturedRequest c = last();
        assertEquals("DELETE", c.method);
        assertEquals("/api/v1/clients/c1/groups/test group", c.path);
        assertEquals("/api/v1/clients/c1/groups/test%20group", c.rawPath);
        assertEquals("clearHistory=false", c.query);
    }

    @Test
    void apiKeyOmittedWhenEmpty() {
        WeChatAutoClient noKey = new WeChatAutoClient("http://127.0.0.1:" + port, "");
        noKey.getClients();

        CapturedRequest c = last();
        assertNull(c.apiKey, "X-API-Key header must not be sent when api key is empty");
    }

    @Test
    void non2xxThrowsWeChatAutoException() throws IOException {
        HttpServer failing = HttpServer.create(new InetSocketAddress("127.0.0.1", 0), 0);
        failing.createContext("/", exchange -> {
            byte[] resp = "boom".getBytes(StandardCharsets.UTF_8);
            exchange.sendResponseHeaders(500, resp.length);
            try (OutputStream os = exchange.getResponseBody()) {
                os.write(resp);
            }
        });
        failing.start();
        try {
            WeChatAutoClient c = new WeChatAutoClient("http://127.0.0.1:" + failing.getAddress().getPort(), "");
            WeChatAutoException ex = assertThrows(WeChatAutoException.class, c::getClients);
            assertEquals(500, ex.getStatusCode());
            assertTrue(ex.getMessage().contains("boom"));
        } finally {
            failing.stop(0);
        }
    }
}
