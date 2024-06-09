using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using EComerence.Core.Domain;
using EComerence.Core.Repositories;
using EComerence.Infrastructure.DTO;
using EComerence.Infrastructure.Extensions;
using EComerence.Infrastructure.Repositories;

namespace EComerence.Infrastructure.Services
{
    public class OrderListService : IOrderListService
    {
        private readonly IOrderListRepository _orderListRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderListService(IOrderListRepository orderListRepository,
            IMapper mapper,
            IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _orderListRepository = orderListRepository;
            _orderRepository = orderRepository;
        }
        public async Task AddAsync(Guid id, Guid userId)
        {
            var @orderList = await _orderListRepository.GetAsync(id) ?? new OrderList(id, userId);
            await _orderListRepository.AddAsync(@orderList);
        }

        public async Task AddOrderAsync(Guid userId, Guid productId, ushort amount)
        {
            var @product = await _productRepository.GetAsync(productId);
            Guid orderListId;
            OrderList orderList = await _orderListRepository.GetCurrentOrderAsync(userId);
            if (orderList == null)
            {
                orderListId = Guid.NewGuid();
                orderList = new OrderList(orderListId, userId);
                orderList.AddProduct(product, amount);
                await _orderListRepository.AddAsync(@orderList);
            }

            if (orderList.Orders.Keys.All(x => x.Id == productId))
            {
                if (amount > 0)
                {
                    @orderList.UpdateOrder(product, amount);
                }
                else orderList.RemoveProduct(product.Id);
                await _orderListRepository.SaveAsync(orderList);
            }
            else if (amount > 0)
            {
                orderList.AddProduct(product, amount);
                await _orderListRepository.SaveAsync(@orderList);
            }
            else throw new Exception($"The amount must be greater than zero, amount: {amount}");
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
        public async Task<OrderListDto> GetCurrentAsync(Guid userId)
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

        public async Task DeleteOrderAsync(Guid id, Guid productId)
        {
            var @orderList = await _orderListRepository.GetAsync(id);
            @orderList.RemoveProduct(productId);
        }

        public async Task<OrderListDto> GetAsync(Guid id)
        {
            var @orderList = await _orderListRepository.GetOrFailAsync(id);
            return _mapper.Map<OrderListDto>(@orderList);
        }
        public async Task SubmitOrder(Guid userId)
        {
            var orderLists = await _orderListRepository.BrowseAsync(userId);
            var orderList = orderLists.SingleOrDefault(x => x.Purchased == false);
            orderList.PucharseProducts();
            List<Order> orders = new();
            foreach (var order in orderList.Orders)
            {
                orders.Add(new Order(
                    userId,
                    order.Key.Id,
                    order.Key.ProducerName,
                    order.Key.Price,
                    order.Value,
                    order.Key.BrandTag));
            }
            await _orderRepository.AddBulkAsync(orders);
            await _orderListRepository.SaveAsync(orderList);
        }
        public async Task<IList<(Guid, Guid)>> GetOrders()
        {
            var orders = await _orderListRepository.BrowseAllAsync();
            List<(Guid, Guid)> pairs = new();

            foreach (var order in orders)
            {
                pairs.AddRange(GetUniqueProductPairs(order));
            }
            return pairs;
        }
        private static List<(Guid, Guid)> GetUniqueProductPairs(OrderList orderList)
        {
            HashSet<Guid> productIds = new HashSet<Guid>();
            List<(Guid, Guid)> uniquePairs = new List<(Guid, Guid)>();


            foreach (var product in orderList.Orders.Keys)
            {
                if (!productIds.Contains(product.Id))
                {
                    productIds.Add(product.Id);
                }

                foreach (var existingProductId in productIds)
                {
                    if (existingProductId != product.Id)
                    {
                        uniquePairs.Add((existingProductId, product.Id));
                    }
                }
            }
            return uniquePairs;
        }
    }
}
