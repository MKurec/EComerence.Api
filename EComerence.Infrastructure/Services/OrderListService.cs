using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EComerence.Core.Domain;
using EComerence.Core.Repositories;
using EComerence.Infrastructure.DTO;
using EComerence.Infrastructure.Extensions;

namespace EComerence.Infrastructure.Services
{
    public class OrderListService : IOrderListService
    {
        private readonly IOrderListRepository _orderListRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public OrderListService(IOrderListRepository orderListRepository,IMapper mapper,IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _orderListRepository = orderListRepository;
        }
        public async Task AddAsync(Guid id, Guid userId)
        {
            var @orderList = await _orderListRepository.GetAsync(id);
            @orderList = new OrderList(id, userId);
            await _orderListRepository.AddAsync(@orderList);
        }

        public async Task AddOrderAsync(Guid userId, Guid productId, int amount)
        {
            var @product = await _productRepository.GetAsync(productId);
            Guid orderListId;
            var orderLists = await BrowseAsync(userId);
            var thisorderList = orderLists.SingleOrDefault(x => x.Purchased == false);
            if (thisorderList == null)
            {
                orderListId = Guid.NewGuid();
                await AddAsync(orderListId, userId);               
            }
            else orderListId = thisorderList.Id;

            var @orderList = await _orderListRepository.GetAsync(orderListId);
            @orderList.AddOrder(@product, amount);
            await _orderListRepository.UpdateAsync(@orderList);
        }

        public async Task<IEnumerable<OrderListDto>> BrowseAsync(Guid userId)
        {
            var orderLists = await _orderListRepository.BrowseAsync(userId);
            return _mapper.Map<IEnumerable<OrderListDto>>(orderLists);
        }

        public async Task<IEnumerable<OrderDto>> BrowseOrdersAsync(Guid orderListId)
        {
            var orderList = await _orderListRepository.GetOrFailAsync(orderListId);
            var orders = _mapper.Map<IEnumerable<OrderDto>>(orderList.Orders).ToList();
            return orders;
        }

        public async Task DeleteAsync(Guid id)
        {
            var @orderList = await _orderListRepository.GetOrFailAsync(id);
            await _orderListRepository.DeleteAsync(@orderList);
        }

        public async Task DeleteOrderAsync(Guid id, Guid orderId)
        {
            var @orderList = await _orderListRepository.GetAsync(id);
            @orderList.RemoveOrder(orderId);
        }

        public async Task<OrderListDto> GetAsync(Guid id)
        {
            var @orderList = await _orderListRepository.GetOrFailAsync(id);
            return _mapper.Map<OrderListDto>(@orderList);
        }
    }
}
