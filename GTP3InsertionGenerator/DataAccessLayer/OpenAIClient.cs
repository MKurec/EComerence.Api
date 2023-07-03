using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LayeredExample.DataAccessLayer
{
   internal class OpenAIClient
   {
      private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
      {
         DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
         PropertyNameCaseInsensitive = true,
         PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

      };

      private string apiKey;

      public OpenAIClient(string apiKey) => this.apiKey = apiKey;

      public string Model { get; set; }
      public double Temperature { get; set; }

      public async Task<string> Completion(string prompt)
      {
         List<Message> messages = new() { };

         var requirements = File.ReadAllText("requirements.txt");
         messages.Add(new Message("system" ,requirements));
         messages.Add( new Message("user", prompt));

         string requestUrl = $"https://api.openai.com/v1/chat/completions";
         HttpClient client = new HttpClient();
         client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

         var requestJson = new
         {
            model = Model,
            messages = messages.ToArray(),
            temperature = Temperature,
         };

         StringContent content = new StringContent(JsonSerializer.Serialize(requestJson, _jsonOptions), Encoding.UTF8, "application/json");

         // Send the request and receive the response
         HttpResponseMessage response = client.PostAsync(requestUrl, content).Result; 
         string responseJson = response.Content.ReadAsStringAsync().Result;

         // Extract the completed text from the response
         var responseObject = JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(responseJson);
         string completedText = (string)responseObject["choices"]?[0]?["message"]?["content"] ?? "";

         return await Task<string>.FromResult(completedText);
      }
   }
   public class Message
   {
      public string Role { get; set; }
      public string Content { get; set; }

      public Message(string role,string message) 
      {
         Role = role;
         Content = message;
      }
   }
}