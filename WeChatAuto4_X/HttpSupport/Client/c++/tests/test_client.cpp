// Unit tests for the generated WeChatAuto C++ client.
// No live server required: a FakeTransport records requests and returns canned bodies.
// Uses a tiny hand-rolled assertion framework (no external test framework).

#include <wechatauto/wechatauto_client.hpp>

#include <iostream>
#include <string>

using wechatauto::HttpResponse;
using wechatauto::HttpTransport;

namespace {

class FakeTransport : public HttpTransport {
public:
    struct RecordedRequest {
        std::string method;
        std::string url;
        std::map<std::string, std::string> headers;
        std::string body;
    };

    std::vector<RecordedRequest> requests;
    std::string response_body = R"({"ok":true})";
    long status_code = 200;

    HttpResponse request(const std::string& method, const std::string& url,
                         const std::map<std::string, std::string>& headers,
                         const std::string& body) override {
        requests.push_back(RecordedRequest{method, url, headers, body});
        HttpResponse resp;
        resp.status_code = status_code;
        resp.body = response_body;
        return resp;
    }

    const RecordedRequest& last() const { return requests.back(); }

    bool has_header(const std::string& name) const {
        return requests.back().headers.find(name) != requests.back().headers.end();
    }

    std::string header(const std::string& name) const {
        auto it = requests.back().headers.find(name);
        return it == requests.back().headers.end() ? std::string() : it->second;
    }
};

int g_failures = 0;

void check(bool cond, const char* expr, int line) {
    if (!cond) {
        std::cerr << "FAIL: " << expr << " (line " << line << ")\n";
        ++g_failures;
    }
}

#define CHECK(cond) check((cond), #cond, __LINE__)

template <typename A, typename B>
void check_eq(const A& actual, const B& expected, const char* expr, int line) {
    if (!(actual == expected)) {
        std::cerr << "FAIL: " << expr << " (line " << line << ")\n";
        ++g_failures;
    }
}

#define CHECK_EQ(actual, expected) check_eq((actual), (expected), #actual " == " #expected, __LINE__)

nlohmann::json parse_body(const std::string& body) {
    return nlohmann::json::parse(body);
}

}  // namespace

int main() {
    using namespace wechatauto;

    // --- 1. GET /clients with API key -------------------------------------------------
    {
        auto transport = std::make_unique<FakeTransport>();
        FakeTransport* fake = transport.get();
        WeChatAutoClient client("http://localhost:5000", "secret-key", std::move(transport));

        client.getClients();

        CHECK_EQ(fake->last().method, "GET");
        CHECK_EQ(fake->last().url, "http://localhost:5000/api/v1/clients");
        CHECK_EQ(fake->header("X-API-Key"), "secret-key");
        CHECK(fake->last().body.empty());
    }

    // --- 2. GET /clients/{clientId} with URL-encoded path param -----------------------
    {
        auto transport = std::make_unique<FakeTransport>();
        FakeTransport* fake = transport.get();
        WeChatAutoClient client("http://localhost:5000", "k", std::move(transport));

        client.getClient("client 1/2");

        CHECK_EQ(fake->last().method, "GET");
        CHECK_EQ(fake->last().url, "http://localhost:5000/api/v1/clients/client%201%2F2");
    }

    // --- 3. GET /friends with fromCache query (default true) --------------------------
    {
        auto transport = std::make_unique<FakeTransport>();
        FakeTransport* fake = transport.get();
        WeChatAutoClient client("http://localhost:5000", "", std::move(transport));

        client.getFriends("c1");

        CHECK_EQ(fake->last().method, "GET");
        CHECK_EQ(fake->last().url, "http://localhost:5000/api/v1/clients/c1/friends?fromCache=true");
        CHECK(!fake->has_header("X-API-Key"));  // api key omitted when empty
    }

    // --- 4. POST /messages/send — snake_case JSON body ---------------------------------
    {
        auto transport = std::make_unique<FakeTransport>();
        FakeTransport* fake = transport.get();
        WeChatAutoClient client("http://localhost:5000", "k", std::move(transport));

        SendMessageRequest req;
        req.who = "alice";
        req.message = "hello";
        req.at_users = std::vector<std::string>{"bob", "carol"};

        client.sendMessage("c1", req);

        CHECK_EQ(fake->last().method, "POST");
        CHECK_EQ(fake->last().url, "http://localhost:5000/api/v1/clients/c1/messages/send");
        CHECK_EQ(fake->header("Content-Type"), "application/json");

        nlohmann::json body = parse_body(fake->last().body);
        CHECK_EQ(body.at("who").get<std::string>(), "alice");
        CHECK_EQ(body.at("message").get<std::string>(), "hello");
        CHECK(body.contains("at_users"));
        CHECK_EQ(body.at("at_users").size(), static_cast<std::size_t>(2));
        CHECK(!body.contains("refer"));  // unset nested object omitted
    }

    // --- 5. POST /friends/add — nested options serialization --------------------------
    {
        auto transport = std::make_unique<FakeTransport>();
        FakeTransport* fake = transport.get();
        WeChatAutoClient client("http://localhost:5000", "k", std::move(transport));

        AddFriendsRequest req;
        req.friends = std::vector<std::string>{"13800000000"};
        req.options = AddFriendsOptions{};
        req.options->interval_time = 5;
        req.options->is_close_win = true;
        req.options->say_hi = "hi";

        client.addFriends("c1", req);

        nlohmann::json body = parse_body(fake->last().body);
        CHECK(body.contains("options"));
        CHECK_EQ(body.at("options").at("interval_time").get<int>(), 5);
        CHECK_EQ(body.at("options").at("is_close_win").get<bool>(), true);
        CHECK_EQ(body.at("options").at("say_hi").get<std::string>(), "hi");
    }

    // --- 6. GET /messages/history with optional query params --------------------------
    {
        auto transport = std::make_unique<FakeTransport>();
        FakeTransport* fake = transport.get();
        WeChatAutoClient client("http://localhost:5000", "", std::move(transport));

        client.getMessageHistory("c1", std::string("alice"), std::nullopt, std::string("2024-01-01"), std::string("2024-02-01"));

        CHECK_EQ(fake->last().url,
                 "http://localhost:5000/api/v1/clients/c1/messages/history?who=alice&start_date=2024-01-01&end_date=2024-02-01");
    }

    // --- 7. DELETE /friends/{nickName} — DELETE method and encoded path ---------------
    {
        auto transport = std::make_unique<FakeTransport>();
        FakeTransport* fake = transport.get();
        WeChatAutoClient client("http://localhost:5000", "", std::move(transport));

        client.removeFriend("c1", "张三");

        CHECK_EQ(fake->last().method, "DELETE");
        CHECK_EQ(fake->last().url, "http://localhost:5000/api/v1/clients/c1/friends/%E5%BC%A0%E4%B8%89");
    }

    // --- 8. POST /groups — PUT-like body and enum serialization -----------------------
    {
        auto transport = std::make_unique<FakeTransport>();
        FakeTransport* fake = transport.get();
        WeChatAutoClient client("http://localhost:5000", "k", std::move(transport));

        SwitchNavigationRequest req;
        req.navigation_type = NavigationType::Chat;

        client.switchNavigation("c1", req);

        CHECK_EQ(fake->last().method, "POST");
        CHECK_EQ(fake->last().url, "http://localhost:5000/api/v1/clients/c1/navigation/switch");
        nlohmann::json body = parse_body(fake->last().body);
        CHECK_EQ(body.at("navigation_type").get<std::string>(), "聊天");
    }

    // --- 9. Custom base URL with trailing slash is normalized -------------------------
    {
        auto transport = std::make_unique<FakeTransport>();
        FakeTransport* fake = transport.get();
        WeChatAutoClient client("http://127.0.0.1:6000/", "", std::move(transport));

        client.getClients();

        CHECK_EQ(fake->last().url, "http://127.0.0.1:6000/api/v1/clients");
    }

    if (g_failures == 0) {
        std::cout << "All tests passed\n";
        return 0;
    }
    std::cout << g_failures << " test(s) failed\n";
    return 1;
}
