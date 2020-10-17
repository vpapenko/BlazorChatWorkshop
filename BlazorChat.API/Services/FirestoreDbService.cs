using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorChat.API.Services
{
    public class FirestoreDbService : IDbService
    {
        private readonly FirestoreDb _db;

        public FirestoreDbService()
        {
            var projectId = Environment.GetEnvironmentVariable("PROJECT_ID");
            _db = FirestoreDb.Create(projectId);
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetMessages(int maxMessages)
        {
            var messages = new List<Dictionary<string, object>>();
            var messageQuery = _db.Collection("messages").OrderByDescending("date").Limit(maxMessages);
            var snapshot = await messageQuery.GetSnapshotAsync();

            foreach (var document in snapshot.Documents)
            {
                var documentDictionary = document.ToDictionary();
                documentDictionary["date"] = ((Timestamp)documentDictionary["date"]).ToDateTime();
                messages.Add(documentDictionary);
            }
            return messages;
        }

        public async Task SaveMessage(string text, string name, DateTime date)
        {
            var docRef = _db.Collection("messages").Document();
            var messageDict = new Dictionary<string, object>
            {
                { "text", text },
                { "name", name },
                { "date", Timestamp.FromDateTime(date) }
            };
            await docRef.SetAsync(messageDict);
        }
    }
}
