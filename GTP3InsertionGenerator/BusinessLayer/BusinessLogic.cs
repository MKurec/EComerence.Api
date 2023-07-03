using LayeredExample.DataAccessLayer;

namespace LayeredExample.BusinessLayer
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IChatbot _chatbot;

        public BusinessLogic(IChatbot chatbot)
        {
            _chatbot = chatbot;
        }

        public string ProcessData(string data)
        {
            string response = _chatbot.GetResponse(data).Result;

            return response;
        }
    }
}
