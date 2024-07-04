using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredExample.DataAccessLayer
{
    public class Chatbot : IChatbot
    {
        private readonly OpenAIClient _client;

        public Chatbot(string apiKey)
        {
            _client = new OpenAIClient(apiKey);
            _client.Model = "gpt-3.5-turbo-1106";
            _client.Temperature = 0.1;
        }

        public async Task<string> GetResponse(string prompt)
        {
            return await _client.Completion(prompt);
        }
    }
}
