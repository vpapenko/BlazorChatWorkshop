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
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly FirestoreService _firestoreService;

        public ChatController(ILogger<ChatController> logger, FirestoreService firestoreService)
        {
            _logger = logger;
            _firestoreService = firestoreService;
        }

        [HttpGet]
        public async Task<IEnumerable<Message>> Get()
        {
            var maxMessages = int.Parse(Environment.GetEnvironmentVariable("MAX_MESSAGES"));
            var messageDictionary = await _firestoreService.GetMessages(maxMessages);
            var messages = messageDictionary.Select(m => new Message()
            {
                Text = (string)m["text"],
                Name = (string)m["name"],
                Date = (DateTime)m["date"],
            });
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
            await _firestoreService.SaveMessage(message.Text, message.Name, DateTime.UtcNow);
        }
    }
}
