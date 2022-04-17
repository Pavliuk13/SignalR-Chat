using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Hubs
{
    public class MessageHub : Hub
    {
        public Task SendMessage(string message)
        {
            return Clients.Others.SendAsync("Send", message);
        }
    }
}