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
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public OrderListService(IOrderListRepository orderListRepository, IMapper mapper, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _orderListRepository = orderListRepository;
            _orderRepository = orderRepository;
        }
        public async Task AddAsync(Guid id, Guid userId)
        {
            var @orderList = await _orderListRepository.GetAsync(id);
            @orderList = new OrderList(id, userId);
            await _orderListRepository.AddAsync(@orderList);
        }

        public async Task AddOrderAsync(Guid Id, Guid userId, Guid productId, int amount)
        {
            var @product = await _productRepository.GetAsync(productId);
            Guid orderListId;
            var orderLists = await BrowseAllAsync(userId);
            var thisorderList = orderLists.SingleOrDefault(x => x.Purchased == false);
            if (thisorderList == null)
            {
                orderListId = Guid.NewGuid();
                await AddAsync(orderListId, userId);
            }
            else orderListId = thisorderList.Id;

            var @orderList = await _orderListRepository.GetAsync(orderListId);
            if (orderList.Orders.Any(x => x.ProductId == product.Id))
            {
                var @order = orderList.Orders.SingleOrDefault(x => x.ProductId == product.Id);
                if (amount > 0)
                {
                    @orderList.UpdateOrder(order.Id, amount);
                    await _orderRepository.UpdateAsync(@order);
                }
                else await DeleteOrderAsync(orderListId, order.Id);
            }
            else if (amount > 0)
            {
                var @order = new Order(Id, @orderList, productId, product.Name, product.Price, amount);
                orderList.AddOrder(@order);
                await _orderRepository.AddAsync(@order);
            }
            else throw new Exception($"The amount must be greater than zero, amount: {amount}");
            await _orderListRepository.UpdateAsync(@orderList);
        }

        public async Task<IEnumerable<OrderListDto>> BrowseAllAsync(Guid userId)
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
        public async Task<OrderListDto> BrowseAsync(Guid userId)
        {
            var orderLists = await _orderListRepository.BrowseAsync(userId);
            var orderList = orderLists.SingleOrDefault(x => x.Purchased == false);
            return _mapper.Map<OrderListDto>(@orderList);
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
            await _orderListRepository.UpdateAsync(@orderList);
        }

        public async Task<OrderListDto> GetAsync(Guid id)
        {
            var @orderList = await _orderListRepository.GetOrFailAsync(id);
            return _mapper.Map<OrderListDto>(@orderList);
        }
    }
}
