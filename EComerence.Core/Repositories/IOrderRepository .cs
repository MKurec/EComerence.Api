using EComerence.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Core.Repositories
{
    public interface IOrderRepository
    {
        Task AddBulkAsync(IEnumerable<Order> order);
        Task<IEnumerable<Order>> BrowseeAllAsync();
    }
}
