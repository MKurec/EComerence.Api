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
        private DbSet<Order> orders;
        public OrderRepository(DbContext context)
        {
            this.Context = context;
            this.orders= Context.Set<Order>();
        }



 

        public async Task AddAsync(Order @order)
        {
            orders.Add(@order);
            Context.SaveChanges();
            await Task.CompletedTask;
        }
        public async Task UpdateAsync(Order @order)
        {
            Context.Entry(@order).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            await Task.CompletedTask;

        }
        public async Task DeleteAsync(Order @order)
        {
            orders.Remove(@order);
            Context.SaveChanges();
            await Task.CompletedTask;

        }


    }
}
