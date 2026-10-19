import { test } from 'node:test';
import assert from 'node:assert/strict';
import { WeChatAutoClient } from '../src/index.js';

/** Build a fake fetch that records calls and returns a canned JSON response. */
function fakeFetch(records, responseBody = { ok: true }, status = 200) {
  return async (url, options) => {
    records.push({ url, options });
    return new Response(JSON.stringify(responseBody), {
      status,
      headers: { 'Content-Type': 'application/json' },
    });
  };
}

test('getClients sends GET to /api/v1/clients without body', async () => {
  const records = [];
  const client = new WeChatAutoClient('http://localhost:5000', '', fakeFetch(records));
  await client.getClients();

  assert.equal(records.length, 1);
  assert.equal(records[0].url, 'http://localhost:5000/api/v1/clients');
  assert.equal(records[0].options.method, 'GET');
  assert.equal(records[0].options.body, undefined);
});

test('sets X-API-Key header when apiKey is non-empty', async () => {
  const records = [];
  const client = new WeChatAutoClient('http://localhost:5000', 'secret-key', fakeFetch(records));
  await client.getClients();

  assert.equal(records[0].options.headers['X-API-Key'], 'secret-key');
});

test('omits X-API-Key header when apiKey is empty', async () => {
  const records = [];
  const client = new WeChatAutoClient('http://localhost:5000', '', fakeFetch(records));
  await client.getClients();

  assert.equal('X-API-Key' in records[0].options.headers, false);
});

test('sendMessage posts a JSON body with Content-Type application/json', async () => {
  const records = [];
  const client = new WeChatAutoClient('http://localhost:5000', 'k', fakeFetch(records));
  const body = { who: 'alice', message: 'hello', at_users: ['bob'] };
  await client.sendMessage('c1', body);

  assert.equal(records[0].url, 'http://localhost:5000/api/v1/clients/c1/messages/send');
  assert.equal(records[0].options.method, 'POST');
  assert.equal(records[0].options.headers['Content-Type'], 'application/json');
  assert.equal(records[0].options.headers['X-API-Key'], 'k');
  assert.deepEqual(JSON.parse(records[0].options.body), body);
});

test('getFriends applies spec default fromCache=true', async () => {
  const records = [];
  const client = new WeChatAutoClient('http://localhost:5000', '', fakeFetch(records));
  await client.getFriends('c1');

  assert.equal(records[0].url, 'http://localhost:5000/api/v1/clients/c1/friends?fromCache=true');
});

test('getFriends honors explicit fromCache=false', async () => {
  const records = [];
  const client = new WeChatAutoClient('http://localhost:5000', '', fakeFetch(records));
  await client.getFriends('c1', false);

  assert.equal(records[0].url, 'http://localhost:5000/api/v1/clients/c1/friends?fromCache=false');
});

test('addFriends posts an array body verbatim (snake_case passthrough)', async () => {
  const records = [];
  const client = new WeChatAutoClient('http://localhost:5000', '', fakeFetch(records));
  const body = {
    friends: ['13800000000'],
    options: { interval_time: 5, is_close_win: false, say_hi: 'hi', suffix: 'test' },
  };
  await client.addFriends('c1', body);

  assert.equal(records[0].url, 'http://localhost:5000/api/v1/clients/c1/friends/add');
  assert.deepEqual(JSON.parse(records[0].options.body), body);
});

test('URL-encodes path parameters with special characters', async () => {
  const records = [];
  const client = new WeChatAutoClient('http://localhost:5000', '', fakeFetch(records));
  await client.getFriendCache('c1', 'a/b?c&d');

  assert.equal(records[0].url, 'http://localhost:5000/api/v1/clients/c1/friends/cache/a%2Fb%3Fc%26d');
});

test('URL-encodes query values (colon and plus in a date-time string)', async () => {
  const records = [];
  const client = new WeChatAutoClient('http://localhost:5000', '', fakeFetch(records));
  await client.getMessagesHistory('c1', '张三', '2026-10-19T10:00:00+08:00');

  assert.ok(records[0].url.includes('who=%E5%BC%A0%E4%B8%89'));
  assert.ok(records[0].url.includes('date=2026-10-19T10%3A00%3A00%2B08%3A00'));
});

test('throws an Error with status and body text on non-2xx', async () => {
  const records = [];
  const client = new WeChatAutoClient('http://localhost:5000', '', fakeFetch(records, { error: 'boom' }, 400));
  await assert.rejects(
    () => client.getClients(),
    (err) => {
      assert.ok(err instanceof Error);
      assert.match(err.message, /HTTP 400/);
      assert.match(err.message, /boom/);
      return true;
    },
  );
});
