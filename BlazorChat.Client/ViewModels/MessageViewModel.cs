using BlasorChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorChat.ViewModels
{
    public class MessageViewModel
    {
        public MessageViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly HttpClient _httpClient;
        public IEnumerable<Message> Messages;


        public async Task LoadMessages()
        {
            Messages = await _httpClient.GetFromJsonAsync<Message[]>("https://blazor-chat.ew.r.appspot.com/api/chat");
        }

        public async Task SendMessage(string message, string name)
        {
            await _httpClient.PutAsJsonAsync<Message>("https://blazor-chat.ew.r.appspot.com/api/chat", new Message() { Text = message, Name = name });
            await LoadMessages();
        }
    }
}
