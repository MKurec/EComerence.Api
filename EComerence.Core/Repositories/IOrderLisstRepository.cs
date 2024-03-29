﻿using EComerence.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Core.Repositories
{
    public interface IOrderListRepository
    {
        Task<OrderList> GetAsync(Guid id);
        Task<IEnumerable<OrderList>> BrowseAsync(Guid userId);
        Task<IEnumerable<OrderList>> BrowseAllAsync();
        Task<OrderList> GetCurrentOrderAsync(Guid userId);

        Task AddAsync(OrderList @orderList);
        Task SaveAsync(OrderList @orderList);
        Task DeleteAsync(OrderList @orderList);
    }
}
