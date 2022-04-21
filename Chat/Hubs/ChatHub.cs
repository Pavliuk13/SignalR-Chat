using System.Threading.Tasks;
using Chat.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string userName, string message)
        {
            return Clients.All.SendAsync("ReceiveOne", userName, message);
        }
    }
}