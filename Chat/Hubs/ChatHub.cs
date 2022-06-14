using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.DTOs;
using Chat.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationContext _context;
        private readonly IDictionary<string, ConnectionDto> _connections;
        private readonly string _bot;

        public ChatHub(ApplicationContext context, IDictionary<string, ConnectionDto> connections)
        {
            _context = context;
            _connections = connections;
            _bot = "ChatBot";
        }

        public async Task JoinRoom(ConnectionDto connectionDto)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connectionDto.Room);

            _connections[Context.ConnectionId] = connectionDto;
            
            await Clients.Group(connectionDto.Room).SendAsync("ReceiveMessage", _bot, $"{connectionDto.User.UserName} has joined room {connectionDto.Room}");
        }
        
        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out var connectionDto))
            {
                await Clients.Group(connectionDto.Room).SendAsync("ReceiveOne", connectionDto.User, message);
                await Clients.Group(connectionDto.Room).SendAsync("ReceiveMessage", _bot, $"{connectionDto.User.UserName} has left");
            }
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out var connectionDto))
            {
                _connections.Remove(Context.ConnectionId);
            }
            
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendConnectedUsers(string room)
        {
            var users = _connections.Values.Where(el => el.Room == room).Select(el => el.User.UserName);
            await Clients.Group(room).SendAsync("UsersInRoom", users);
        }
    }
}