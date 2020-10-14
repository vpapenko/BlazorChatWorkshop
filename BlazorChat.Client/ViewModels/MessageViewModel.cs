using BlasorChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorChat.ViewModels
{
    public class MessageViewModel
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public static implicit operator MessageViewModel(Message message)
        {
            return new MessageViewModel()
            {
                Text = message.Text,
                Name = message.Name,
                Date = message.Date
            };
        }
    }
}
