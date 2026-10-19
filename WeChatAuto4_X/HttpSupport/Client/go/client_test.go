package client

import (
	"encoding/json"
	"io"
	"net/http"
	"net/http/httptest"
	"net/url"
	"testing"
)

type record struct {
	method      string
	path        string
	escapedPath string
	requestURI  string
	rawQuery    string
	query       url.Values
	apiKey      string
	contentType string
	body        []byte
}

func newTestServer(t *testing.T, apiKey string) (*httptest.Server, *record, *Client) {
	t.Helper()
	rec := &record{}
	srv := httptest.NewServer(http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		body, _ := io.ReadAll(r.Body)
		rec.method = r.Method
		rec.path = r.URL.Path
		rec.escapedPath = r.URL.EscapedPath()
		rec.requestURI = r.RequestURI
		rec.rawQuery = r.URL.RawQuery
		rec.query = r.URL.Query()
		rec.apiKey = r.Header.Get("X-API-Key")
		rec.contentType = r.Header.Get("Content-Type")
		rec.body = body

		w.Header().Set("Content-Type", "application/json")
		w.WriteHeader(http.StatusOK)
		_, _ = w.Write([]byte(`{"ok":true}`))
	}))
	t.Cleanup(srv.Close)
	return srv, rec, NewClient(srv.URL, apiKey, nil)
}

func TestListClients(t *testing.T) {
	_, rec, cl := newTestServer(t, "secret-key")
	body, err := cl.ListClients()
	if err != nil {
		t.Fatalf("ListClients: %v", err)
	}
	if rec.method != http.MethodGet {
		t.Errorf("method = %q, want GET", rec.method)
	}
	if rec.path != "/api/v1/clients" {
		t.Errorf("path = %q, want /api/v1/clients", rec.path)
	}
	if rec.apiKey != "secret-key" {
		t.Errorf("X-API-Key = %q, want secret-key", rec.apiKey)
	}
	if len(rec.body) != 0 {
		t.Errorf("body = %q, want empty", string(rec.body))
	}
	if string(body) != `{"ok":true}` {
		t.Errorf("response body = %q", string(body))
	}
}

func TestGetClientPathParam(t *testing.T) {
	_, rec, cl := newTestServer(t, "secret-key")
	if _, err := cl.GetClient("c-123"); err != nil {
		t.Fatalf("GetClient: %v", err)
	}
	if rec.method != http.MethodGet {
		t.Errorf("method = %q, want GET", rec.method)
	}
	if rec.path != "/api/v1/clients/c-123" {
		t.Errorf("path = %q, want /api/v1/clients/c-123", rec.path)
	}
}

func TestListFriendsDefaultFromCache(t *testing.T) {
	_, rec, cl := newTestServer(t, "secret-key")
	if _, err := cl.ListFriends("c-123"); err != nil {
		t.Fatalf("ListFriends: %v", err)
	}
	if rec.path != "/api/v1/clients/c-123/friends" {
		t.Errorf("path = %q", rec.path)
	}
	if got := rec.query.Get("fromCache"); got != "true" {
		t.Errorf("fromCache = %q, want true", got)
	}
}

func TestSendMessageBody(t *testing.T) {
	_, rec, cl := newTestServer(t, "secret-key")
	who := "alice"
	msg := "hello"
	if _, err := cl.SendMessage("c-123", SendMessageRequest{Who: &who, Message: &msg}); err != nil {
		t.Fatalf("SendMessage: %v", err)
	}
	if rec.method != http.MethodPost {
		t.Errorf("method = %q, want POST", rec.method)
	}
	if rec.path != "/api/v1/clients/c-123/messages/send" {
		t.Errorf("path = %q", rec.path)
	}
	if rec.contentType != "application/json" {
		t.Errorf("Content-Type = %q, want application/json", rec.contentType)
	}
	if rec.apiKey != "secret-key" {
		t.Errorf("X-API-Key = %q, want secret-key", rec.apiKey)
	}
	var m map[string]any
	if err := json.Unmarshal(rec.body, &m); err != nil {
		t.Fatalf("unmarshal body: %v", err)
	}
	if m["who"] != "alice" {
		t.Errorf("body.who = %v, want alice", m["who"])
	}
	if m["message"] != "hello" {
		t.Errorf("body.message = %v, want hello", m["message"])
	}
}

func TestQuitGroupDefaultClearHistory(t *testing.T) {
	_, rec, cl := newTestServer(t, "secret-key")
	if _, err := cl.QuitGroup("c-123", "my group"); err != nil {
		t.Fatalf("QuitGroup: %v", err)
	}
	if rec.method != http.MethodDelete {
		t.Errorf("method = %q, want DELETE", rec.method)
	}
	if rec.path != "/api/v1/clients/c-123/groups/my group" {
		t.Errorf("path = %q", rec.path)
	}
	if got := rec.query.Get("clearHistory"); got != "true" {
		t.Errorf("clearHistory = %q, want true", got)
	}
}

func TestGetMessageHistoryQuery(t *testing.T) {
	_, rec, cl := newTestServer(t, "secret-key")
	if _, err := cl.GetMessageHistory("c-123", "bob", "2024-01-01", "2024-01-01", "2024-02-01"); err != nil {
		t.Fatalf("GetMessageHistory: %v", err)
	}
	if rec.path != "/api/v1/clients/c-123/messages/history" {
		t.Errorf("path = %q", rec.path)
	}
	for key, want := range map[string]string{
		"who":        "bob",
		"date":       "2024-01-01",
		"start_date": "2024-01-01",
		"end_date":   "2024-02-01",
	} {
		if got := rec.query.Get(key); got != want {
			t.Errorf("query %s = %q, want %q", key, got, want)
		}
	}
}

func TestAPIKeyOmittedWhenEmpty(t *testing.T) {
	_, rec, cl := newTestServer(t, "")
	if _, err := cl.ListClients(); err != nil {
		t.Fatalf("ListClients: %v", err)
	}
	if rec.apiKey != "" {
		t.Errorf("X-API-Key = %q, want empty (header omitted)", rec.apiKey)
	}
}

func TestPathAndQueryEncoding(t *testing.T) {
	_, rec, cl := newTestServer(t, "secret-key")

	// Path segment encoding: clientId and who both contain special characters.
	if _, err := cl.GetFriendCache("client/1", "a/b?c d"); err != nil {
		t.Fatalf("GetFriendCache: %v", err)
	}
	wantURI := "/api/v1/clients/client%2F1/friends/cache/a%2Fb%3Fc%20d"
	if rec.requestURI != wantURI {
		t.Errorf("request URI = %q, want %q", rec.requestURI, wantURI)
	}

	// Query value encoding: space becomes +, & becomes %26.
	if _, err := cl.GetMessageHistory("c-123", "a b&c", "", "", ""); err != nil {
		t.Fatalf("GetMessageHistory: %v", err)
	}
	if got := rec.query.Get("who"); got != "a b&c" {
		t.Errorf("decoded who = %q, want %q", got, "a b&c")
	}
	if rec.rawQuery != "who=a+b%26c" {
		t.Errorf("rawQuery = %q, want %q", rec.rawQuery, "who=a+b%26c")
	}
}
