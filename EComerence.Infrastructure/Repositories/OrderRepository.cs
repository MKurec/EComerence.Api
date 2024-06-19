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
   public class OrderRepository : IOrderRepository
   {
      protected readonly DbContext Context;
      private DbSet<Order> _orders;
      public OrderRepository(DbContext context)
      {
         this.Context = context;
         this._orders = Context.Set<Order>();
      }


      public async Task AddBulkAsync(IEnumerable<Order> orders)
      {
         Context.AddRange(orders);
         await Context.SaveChangesAsync();
      }

      public async Task<IEnumerable<Order>> BrowseeAllAsync()
      {
         var OdrerLists = _orders.AsEnumerable();
         return await Task.FromResult(OdrerLists); ;
      }
   }
}
