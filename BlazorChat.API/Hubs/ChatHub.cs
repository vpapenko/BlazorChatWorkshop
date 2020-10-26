using BlasorChat.Models;
using BlazorChat.API.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorChat.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDbService _dbService;

        public ChatHub(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task SendMessage(Message message)
        {
            if (string.IsNullOrEmpty(message.Text))
            {
                throw new ArgumentNullException(nameof(message.Text));
            }
            if (string.IsNullOrEmpty(message.Name))
            {
                throw new ArgumentNullException(nameof(message.Name));
            }
            message.Date = DateTime.UtcNow;
            await _dbService.SaveMessage(message.Text, message.Name, message.Date);
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
