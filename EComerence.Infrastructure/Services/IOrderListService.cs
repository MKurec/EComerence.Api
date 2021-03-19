using EComerence.Core.Domain;
using EComerence.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Services
{
    public interface IOrderListService 
    {
        Task<OrderListDto> GetAsync(Guid id);
        Task<OrderListDto> BrowseAsync(Guid orderId);
        Task<IEnumerable<OrderListDto>> BrowseAllAsync(Guid userId);
        Task<IEnumerable<OrderDto>> BrowseOrdersAsync(Guid orderListId);
        Task AddAsync(Guid id, Guid userId);
        Task DeleteAsync(Guid id);
        Task DeleteOrderAsync(Guid id, Guid orderId);

        Task AddOrderAsync(Guid Id,Guid userId, Guid productId, int amount);
    }
}
