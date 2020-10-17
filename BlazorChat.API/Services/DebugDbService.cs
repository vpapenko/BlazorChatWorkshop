using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorChat.API.Services
{
    public class DebugDbService : IDbService
    {
        public async Task<IEnumerable<Dictionary<string, object>>> GetMessages(int maxMessages)
        {
            return await Task.Run(() =>
            {
                return Enumerable.Range(1, maxMessages).Select(i => new Dictionary<string, object>()
                {
                    { "text",$"message{i}" },
                    { "name",$"name{i}" },
                    { "date", DateTime.Now.AddDays(-i) }
                });
            });
        }

        public async Task SaveMessage(string text, string name, DateTime date)
        {
            await Task.Run(() => { });
        }
    }
}
