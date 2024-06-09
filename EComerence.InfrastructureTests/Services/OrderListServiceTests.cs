using NUnit.Framework;
using EComerence.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EComerence.Core.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EComerence.Core.Repositories;
using EComerence.Infrastructure.Repositories;
using Moq;
using Microsoft.Extensions.Options;
using EComerence.Infrastructure.Mappers;

namespace EComerence.Infrastructure.Services.Tests
{
    [TestFixture()]
    public class OrderListServiceTests
    {
        private readonly IOrderListService _orderListService;
        private readonly IOrderListRepository _orderListRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private ApplicationDbContext _dbContext;
        private DbContextOptions<ApplicationDbContext> _options;
        public OrderListServiceTests()
        {
            // Configure the in-memory database options
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;

            // Create the DbContext instance
            _dbContext = new ApplicationDbContext(_options);
            _orderListRepository = new OrderListRepository(_dbContext);
            _orderRepository = new OrderRepository(_dbContext);
            _productRepository = new ProductRepository(_dbContext);
            _mapper = AutoMapperConfig.Initialize();

            _orderListService = new OrderListService(
                            _orderListRepository,
                            _mapper,
                            _productRepository,
                            _orderRepository);

        }
        [Test()]
        public void AddOrderAsyncTest()
        {

            Assert.DoesNotThrowAsync(async () =>
               await _orderListService.AddOrderAsync(Guid.Parse("17FF73CC-5AF4-4FE9-9FEF-13F2DB9F90B7"), Guid.Parse("ED0C577E-82C0-467C-9E17-13E068369ADE"), 2)
            );
        }
    }
}