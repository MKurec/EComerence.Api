using EComerence.Core.Repositories;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Services
{

   public interface IUserProductProbabilityService
   {
      public Task<List<Guid>> GetRecomendations(Guid userId, int count, Dictionary<Guid, double> copuchrsedProducts);
   }

   public class UserProductProbabilityService        : IUserProductProbabilityService
   {
      private readonly IUserProductProbabilityRepository _userProductProbabilityRepository;

      public UserProductProbabilityService(IUserProductProbabilityRepository userProductProbabilityRepository) 
      {
         _userProductProbabilityRepository = userProductProbabilityRepository;
      }

      public async Task<List<Guid>> GetRecomendations(Guid userId, int count, Dictionary<Guid,double> copuchrsedProducts)
      {
         var probabilieties = await _userProductProbabilityRepository.GetAsync(userId, 30);
         var prob = probabilieties.Where(x => copuchrsedProducts.ContainsKey(x.ProductId))
            .ToDictionary(x=> x.ProductId ,x => x.Probablity);

         // merge buing probabilities with copuchrsed products by multiplying double probability
         foreach (var item in copuchrsedProducts)
         {
            if (prob.ContainsKey(item.Key))
            {
               prob[item.Key] = prob[item.Key] * item.Value;
            }
         }
         // take top 2 Guids with highest probability
         var top2 = prob.OrderByDescending(x => x.Value)
            .Select(x => x.Key)
            .Take(count)
            .ToList();

         return top2;
      }
   }
}
