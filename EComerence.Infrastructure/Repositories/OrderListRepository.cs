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
    public class OrderListRepository : IOrderListRepository
    {
        protected readonly DbContext Context;
        private DbSet<OrderList> orderLists;
        public OrderListRepository(DbContext context)
        {
            this.Context = context;
            this.orderLists = Context.Set<OrderList>();
        }



        public async Task<OrderList> GetAsync(Guid id)
        {
            var @orderList = await Task.FromResult(Context.Set<OrderList>().Include(x => x.Orders).SingleOrDefault(x => x.Id == id));
            return @orderList;
        }

        public async Task AddAsync(OrderList @orderList)
        {
            orderLists.Add(@orderList);
            Context.SaveChanges();
            await Task.CompletedTask;
        }
        public async Task UpdateAsync(OrderList @orderList)
        {
            Context.Entry(@orderList).State = EntityState.Modified;
            Context.SaveChanges();
            await Task.CompletedTask;

        }
        public async Task DeleteAsync(OrderList @orderList)
        {
            orderLists.Remove(@orderList);
            Context.SaveChanges();
            await Task.CompletedTask;

        }

        public async Task<IEnumerable<OrderList>> BrowseAsync(Guid userId)
        {
            var xOdrerLists = orderLists.AsEnumerable();
            xOdrerLists = xOdrerLists.Where(x => x.UserId == userId);
            return await Task.FromResult(xOdrerLists);
        }
    }
}
