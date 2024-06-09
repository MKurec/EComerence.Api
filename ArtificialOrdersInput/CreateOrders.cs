using EComerence.Core.Repositories;
using EComerence.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArtificialOrdersInput
{
   public class CreateOrders
   {
      private readonly IOrderListService _orderListService;
      private readonly IUsersRepository _userRepository;

      public CreateOrders(IOrderListService orderListService, IUsersRepository userRepository)
      {
         _orderListService = orderListService;
         _userRepository = userRepository;
      }
      public async Task InsertOrders()
      {
         string folderPath = "UserResponses";
         DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
         foreach (var fileInfo in directoryInfo.GetFiles())
         {
            string fileName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            string userName = GetUserName(fileName);
            Guid userId = GetUserIdAsync(userName);


            string json = await File.ReadAllTextAsync(fileInfo.FullName);
            JsonDocument document = JsonDocument.Parse(json);

            foreach (JsonProperty orderList in document.RootElement.EnumerateObject())
            {
               foreach (JsonElement order in orderList.Value.EnumerateArray())
               {
                  Guid productId = Guid.Parse(order.GetProperty("id").GetString());
                  ushort amount = order.GetProperty("amount").GetUInt16();
                  await _orderListService.AddOrderAsync((Guid)userId, productId, amount);
               }
            await _orderListService.SubmitOrder((Guid)userId);

            }


         }
      }

      private Guid GetUserIdAsync(string username)
      {
         var user = _userRepository.GetUsersAsync().Result.SingleOrDefault(x => x.FirstName == username);
         return user?.Id ?? throw new Exception($"Cannot find user with name {username}");
      }

      private static string GetUserName(string fileName)
      {
         int responseIndex = fileName.IndexOf("Response");
         if (responseIndex >= 0)
         {
            return fileName.Substring(0, responseIndex);
         }
         else
         {
            throw new Exception($"Cannot find user name in file name {fileName}");
         }
      }
   }
}
