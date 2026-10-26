using System.Text.Json;
using WeChatAuto.Client;
using WeChatAuto.Client.Models;

namespace WinFormWithHttpWebAPI;

public partial class Form1 : Form
{
    private readonly WeChatAutoClient _client = new();

    public Form1()
    {
        InitializeComponent();
    }

    private async void Button1_Click(object? sender, EventArgs e)
    {
        try
        {
            string clientId = textBox4.Text.Trim();
            if (string.IsNullOrWhiteSpace(clientId))
            {
                MessageBox.Show(this, "请先填写客户端ID。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string groupName = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                MessageBox.Show(this, "请先填写群名。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 客户端返回服务端的原始响应体（群成员列表的 JSON 字符串）。
            string? result = await _client.GetGroupMembersAsync(clientId, groupName);

            listBox1.Items.Clear();
            List<string>? members = string.IsNullOrWhiteSpace(result)
                ? null
                : JsonSerializer.Deserialize<List<string>>(result!);

            if (members != null)
            {
                foreach (string member in members)
                {
                    listBox1.Items.Add(member);
                }
            }

            MessageBox.Show(this, $"共 {listBox1.Items.Count} 个好友", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "获取群成员失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void Button2_Click(object? sender, EventArgs e)
    {
        try
        {
            string clientId = textBox5.Text.Trim();
            if (string.IsNullOrWhiteSpace(clientId))
            {
                MessageBox.Show(this, "请先填写客户端ID。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = textBox3.Text.Trim();
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show(this, "请先填写消息内容。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // who 为空时发送给当前焦点聊天窗口。
            string? who = string.IsNullOrWhiteSpace(textBox2.Text) ? null : textBox2.Text.Trim();

            await _client.SendMessageAsync(clientId, new SendMessageRequest
            {
                Who = who,
                Message = message,
            });

            MessageBox.Show(this, "消息已发送。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "发送消息失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
