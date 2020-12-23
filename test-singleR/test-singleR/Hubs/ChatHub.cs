using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace test_SingalR.Hubs
{
    // Hub 类管理连接、组和消息
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
