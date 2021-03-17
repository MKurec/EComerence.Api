using EComerence.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Core.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order @order);
        Task UpdateAsync(Order @order);
        Task DeleteAsync(Order @order);
    }
}
