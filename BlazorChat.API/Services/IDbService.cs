using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorChat.API.Services
{
    public interface IDbService
    {
        Task<IEnumerable<Dictionary<string, object>>> GetMessages(int maxMessages);
        Task SaveMessage(string text, string name, DateTime date);
    }
}
