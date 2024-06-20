using EComerence.Core.Repositories;
using EComerence.Core;
using EComerence.Infrastructure.Mappers;
using EComerence.Infrastructure.Repositories;
using EComerence.Infrastructure.Services;
using MachineLearningWorker;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using MachineLearningWorker.MachineLearining;
using System.Runtime.CompilerServices;

//IHost host = Host.CreateDefaultBuilder(args)
//    .ConfigureServices(services =>
//    {
//        services.AddHostedService<Worker>();
//        services.AddDbContext<ApplicationDbContext>();
//        services.AddSingleton<DbContext, ApplicationDbContext>();
//        services.AddSingleton<IUnitOfWork, UnitOfWork>();
//        services.AddSingleton<IUserRepository, UserRepository>();
//        services.AddSingleton<IProductRepository, ProductRepository>();
//        services.AddSingleton<ICategoryRepository, CategoryRepository>();
//        services.AddSingleton<IOrderListRepository, OrderListRepository>();
//        services.AddSingleton<IProducerRepository, ProducerRepository>();
//        services.AddSingleton<IFileRepository, FileRepository>();
//        services.AddSingleton<IOrderRepository, OrderRepository>();
//        services.AddSingleton<IOrderListService, OrderListService>();
//       services.AddSingleton<IProductService, ProductService>();
//        services.AddSingleton(AutoMapperConfig.Initialize());
//    })
//    .Build();

//host.Run();
class Program
{
   static async Task Main(string[] args)
   {
      IConfiguration configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: true)
          .Build();

      var services = new ServiceCollection();

      ConfigureServices(services, configuration);

      var serviceProvider = services.BuildServiceProvider();

      var productRepository = serviceProvider.GetService<IProductRepository>();
      var productService= serviceProvider.GetService<IProductService>();

      var orderListService = serviceProvider.GetService<IOrderListService>();

      await TrainCopurchasedProductModel(orderListService, productRepository, productService);

      Console.WriteLine("Application finished.");
      Console.ReadLine();
   }
   private static async Task TrainCopurchasedProductModel(IOrderListService orderListService,IProductRepository productRepository, IProductService productService)
   {

      var task = new MatrixFactoryzation(orderListService, productService, productRepository);
      await task.TrainAsync();


   }

   static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
   {
      services.AddDbContext<ApplicationDbContext>();
      services.AddScoped<DbContext, ApplicationDbContext>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped<ICategoryRepository, CategoryRepository>();
      services.AddScoped<IOrderListRepository, OrderListRepository>();
      services.AddScoped<IProducerRepository, ProducerRepository>();
      services.AddScoped<IFileRepository, FileRepository>();
      services.AddScoped<IOrderRepository, OrderRepository>();
      services.AddScoped<IOrderListService, OrderListService>();
      services.AddScoped<IProductService, ProductService>();
      services.AddScoped<IUserProductProbabilityService, UserProductProbabilityService>();
      services.AddScoped<IUserProductProbabilityRepository, UserProductProbabilityRepository>();

      services.AddSingleton(AutoMapperConfig.Initialize());
   }
}