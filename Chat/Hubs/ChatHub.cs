using System.Threading.Tasks;
using Chat.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        private ApplicationContext _context;
        public ChatHub(ApplicationContext context)
        {
            _context = context;
        }
        public Task SendMessage(string userName, string message)
        {
            return Clients.All.SendAsync("ReceiveOne", userName, message);
        }
    }
}