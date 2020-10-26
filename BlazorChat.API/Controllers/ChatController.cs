using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlasorChat.Models;
using BlazorChat.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlasorChat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IDbService _dbService;

        public ChatController(ILogger<ChatController> logger, IDbService dbService)
        {
            _logger = logger;
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IEnumerable<Message>> Get()
        {
            var maxMessages = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MAX_MESSAGES")) ? 5 : int.Parse(Environment.GetEnvironmentVariable("MAX_MESSAGES"));
            var messageDictionary = await _dbService.GetMessages(maxMessages);
            var messages = messageDictionary.Select(m => new Message()
            {
                Text = (string)m["text"],
                Name = (string)m["name"],
                Date = (DateTime)m["date"],
            }).OrderBy(m => m.Date);
            return messages;
        }

        [HttpPut]
        public async Task Put(Message message)
        {
            if (string.IsNullOrEmpty(message.Text))
            {
                throw new ArgumentNullException(nameof(message.Text));
            }
            if (string.IsNullOrEmpty(message.Name))
            {
                throw new ArgumentNullException(nameof(message.Name));
            }
            await _dbService.SaveMessage(message.Text, message.Name, DateTime.UtcNow);
        }
    }
}
