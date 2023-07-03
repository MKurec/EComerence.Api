using LayeredExample.BusinessLayer;
using LayeredExample.DataAccessLayer;


string apiKey = "";

IChatbot chatbot = new Chatbot(apiKey);

IBusinessLogic businessLogic = new BusinessLogic(chatbot);

string inputFile = "users_needs.txt";
string outputFolder = "UserResponses";

// Create the output folder if it doesn't exist
Directory.CreateDirectory(outputFolder);

foreach (string line in File.ReadLines(inputFile))
{
   // Split the line into user and prompt
   string[] parts = line.Split(':');
   string user = parts[0].Trim();
   string prompt = parts[1].Trim();

   // Generate the output file name
   string outputFile = $"{user}Response.txt";
   string outputPath = Path.Combine(outputFolder, outputFile);

   // Generate the response and save it to the output file
   string response = businessLogic.ProcessData(prompt);
   File.AppendAllText(outputPath, response);
}


