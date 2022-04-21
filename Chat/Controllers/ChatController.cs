using Chat.Hubs;
using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IHubContext<ChatHub> _hub;

        public ChatController(ApplicationContext context, IHubContext<ChatHub> hub)
        {
            _context = context;
            _hub = hub;
        }
        
        [HttpPost]
        [Route("send")]
        public IActionResult Send([FromBody] Message message)
        {
            _hub.Clients.All.SendAsync("ReceiveOne", message.User, message.Text);
            return Ok();
        }
    }
}