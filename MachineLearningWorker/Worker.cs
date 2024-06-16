using EComerence.Core.Repositories;
using EComerence.Infrastructure.Services;
using MachineLearningWorker.MachineLearining;
using Microsoft.Extensions.DependencyInjection;

//namespace MachineLearningWorker
//{
//    public class Worker : BackgroundService
//    {
//        private readonly ILogger<Worker> _logger;
//        private readonly IOrderListService _orderListService;
//        private readonly IProductService _productService;
//        private readonly IProductRepository _productRepository;
//        private Timer _timer;

//        public Worker(ILogger<Worker> logger,
//            IProductRepository productRepository,
//            IOrderListService orderListService,
//            IProductService productService
//            )
//        {
//            _logger = logger;
//            _orderListService = orderListService;
//            _productRepository = productRepository;
//            _productService = productService;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            _timer = new Timer(async (_) => await TrainCopurchasedProductModel(stoppingToken), null, TimeSpan.Zero, TimeSpan.FromDays(1));

//            await Task.Delay(Timeout.Infinite, stoppingToken);

//        }
//        private async Task TrainCopurchasedProductModel(CancellationToken cancellationToken)
//        {

//                var task = new MatrixFactoryzation(_orderListService, _productService, _productRepository);
//                await task.TrainAsync();

//                _logger.LogInformation("Application finished.");
//                await Task.Delay(1000, cancellationToken); 
            
//        }
//        public override async Task StopAsync(CancellationToken cancellationToken)
//        {
//            _timer?.Dispose();
//            await base.StopAsync(cancellationToken);
//        }
//    }
//}