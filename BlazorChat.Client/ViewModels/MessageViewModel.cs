using BlasorChat.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorChat.ViewModels
{
    public class MessageViewModel : INotifyPropertyChanged
    {
        public MessageViewModel(HttpClient httpClient, UserViewModel userViewModel)
        {
            _httpClient = httpClient;
            _userViewModel = userViewModel;
        }

        private HubConnection _hubConnection;
        private readonly HttpClient _httpClient;
        private readonly UserViewModel _userViewModel;

        public List<Message> Messages = new List<Message>();
        public event PropertyChangedEventHandler PropertyChanged;
        public string NewMessage { get; set; }
        public bool MessageCanBeSend => !string.IsNullOrEmpty(NewMessage) && !string.IsNullOrEmpty(_userViewModel.Name);

        public async Task Init()
        {
            var messages = await _httpClient.GetFromJsonAsync<Message[]>("https://blazor-chat.ew.r.appspot.com/api/chat");
            Messages.Clear();
            Messages.AddRange(messages);
            _hubConnection = new HubConnectionBuilder().WithUrl("https://blazor-chat.ew.r.appspot.com/api/chathub").Build();

            _hubConnection.On<Message>("ReceiveMessage", (message) =>
            {
                Messages.Add(message);
                OnPropertyChanged();
            });

            await _hubConnection.StartAsync();
        }

        public async Task SendMessage(string message, string name)
        {
            await _hubConnection.SendAsync("SendMessage", new Message() { Text = message, Name = name });
            NewMessage = "";
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
