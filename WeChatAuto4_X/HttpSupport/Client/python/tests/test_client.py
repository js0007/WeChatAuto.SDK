"""Unit tests for the WECHATAUTO.SDK Python client (no live server)."""

import pytest

from wechatauto.client import WeChatAutoClient, WeChatAutoError
from wechatauto import models


class FakeResponse:
    def __init__(self, status_code=200, json_data=None, text=""):
        self.status_code = status_code
        self._json_data = json_data
        self.text = text

    def json(self):
        return self._json_data


class FakeSession:
    """Records request() calls and returns a canned response."""

    def __init__(self, response=None):
        self.response = response if response is not None else FakeResponse()
        self.calls = []

    def request(self, method, url, **kwargs):
        self.calls.append((method, url, kwargs))
        return self.response


def _last_call(session):
    return session.calls[-1]


# -- representative operations --------------------------------------------


def test_get_clients():
    session = FakeSession(FakeResponse(json_data=[{"id": 1}]))
    client = WeChatAutoClient(base_url="http://localhost:5000", session=session)
    result = client.get_clients()
    method, url, kwargs = _last_call(session)
    assert method == "GET"
    assert url == "http://localhost:5000/api/v1/clients"
    assert kwargs.get("params") is None
    assert kwargs.get("json") is None
    assert result == [{"id": 1}]


def test_get_client_path_param():
    session = FakeSession()
    client = WeChatAutoClient(session=session)
    client.get_client("abc123")
    method, url, kwargs = _last_call(session)
    assert method == "GET"
    assert url == "http://localhost:5000/api/v1/clients/abc123"


def test_get_friends_query_param_default():
    session = FakeSession()
    client = WeChatAutoClient(session=session)
    client.get_friends("c1")
    method, url, kwargs = _last_call(session)
    assert method == "GET"
    assert url == "http://localhost:5000/api/v1/clients/c1/friends"
    assert kwargs["params"] == {"fromCache": True}


def test_send_message_with_dataclass_body():
    session = FakeSession(FakeResponse(json_data={"ok": True}))
    client = WeChatAutoClient(session=session)
    body = models.SendMessageRequest(who="friend", message="hello")
    result = client.send_message("c1", body)
    method, url, kwargs = _last_call(session)
    assert method == "POST"
    assert url == "http://localhost:5000/api/v1/clients/c1/messages/send"
    assert kwargs["json"] == {"who": "friend", "message": "hello"}
    assert result == {"ok": True}


def test_send_message_with_dict_body_passthrough():
    session = FakeSession()
    client = WeChatAutoClient(session=session)
    client.send_message("c1", {"who": "friend", "at_users": ["a", "b"]})
    _, _, kwargs = _last_call(session)
    assert kwargs["json"] == {"who": "friend", "at_users": ["a", "b"]}


def test_leave_group_query_param():
    session = FakeSession()
    client = WeChatAutoClient(session=session)
    client.leave_group("c1", "some group", clear_history=False)
    method, url, kwargs = _last_call(session)
    assert method == "DELETE"
    assert url == "http://localhost:5000/api/v1/clients/c1/groups/some%20group"
    assert kwargs["params"] == {"clearHistory": False}


def test_api_key_header_set_when_non_empty():
    session = FakeSession()
    client = WeChatAutoClient(api_key="secret", session=session)
    client.get_clients()
    _, _, kwargs = _last_call(session)
    assert kwargs["headers"] == {"X-API-Key": "secret"}


def test_api_key_header_omitted_when_empty():
    session = FakeSession()
    client = WeChatAutoClient(api_key="", session=session)
    client.get_clients()
    _, _, kwargs = _last_call(session)
    assert kwargs["headers"] == {}


def test_error_raised_on_non_2xx():
    session = FakeSession(FakeResponse(status_code=500, text="boom"))
    client = WeChatAutoClient(session=session)
    with pytest.raises(WeChatAutoError) as excinfo:
        client.get_clients()
    assert excinfo.value.status_code == 500
    assert "boom" in str(excinfo.value)


# -- dataclass serialization ----------------------------------------------


def test_dataclass_to_dict_omits_none():
    body = models.SendMessageRequest(who="friend")
    assert body.to_dict() == {"who": "friend"}


def test_dataclass_to_dict_nested_and_enum():
    refer = models.ChatRefer(message=models.ChatSimpleMessage(who="x"))
    body = models.SendMessageRequest(
        who="friend", message="hi", refer=refer, at_users=["a"]
    )
    assert body.to_dict() == {
        "who": "friend",
        "message": "hi",
        "at_users": ["a"],
        "refer": {"message": {"who": "x"}},
    }


def test_enum_value_serialization():
    req = models.SwitchNavigationRequest(
        navigation_type=models.NavigationType.CHAT_FILES
    )
    assert req.to_dict() == {"navigation_type": "聊天文件"}


def test_url_encoding_of_path_param_with_slash():
    session = FakeSession()
    client = WeChatAutoClient(session=session)
    client.get_friends_cache_who("c1", "a/b&c")
    method, url, kwargs = _last_call(session)
    assert url == "http://localhost:5000/api/v1/clients/c1/friends/cache/a%2Fb%26c"
