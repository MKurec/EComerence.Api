using EComerence.Core.Domain;
using EComerence.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Repositories
{
   public class UserProductProbabilityRepository : IUserProductProbabilityRepository
   {
      protected readonly DbContext Context;
      private DbSet<UserProductProbability> products;
      public UserProductProbabilityRepository(DbContext context)
      {
         this.Context = context;
         this.products = Context.Set<UserProductProbability>();
      }



      public async Task<List<UserProductProbability>> GetAsync(Guid id, int num)
      {
         var productProbabilities = await Task.FromResult(Context.Set<UserProductProbability>()
            .Where(x => x.UserId == id)
            .OrderBy(x => x.Probablity)
            .Take(num)
            .ToList()
            );
         return productProbabilities;
      }

   }
}
