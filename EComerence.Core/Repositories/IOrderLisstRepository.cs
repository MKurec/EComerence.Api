using EComerence.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Core.Repositories
{
    public interface IOrderListRepository
    {
        Task<OrderList> GetAsync(Guid id);

        Task AddAsync(OrderList product);
        Task UpdateAsync(OrderList product);
        Task DeleteAsync(OrderList product);
    }
}
