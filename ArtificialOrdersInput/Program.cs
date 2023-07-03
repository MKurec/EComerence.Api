using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EComerence.Infrastructure.Repositories;
using EComerence.Core;
using EComerence.Core.Repositories;
using EComerence.Infrastructure.Services;
using EComerence.Infrastructure.Settings;
using EComerence.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using ArtificialOrdersInput;

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

      var usersRepository = serviceProvider.GetService<IUsersRepository>();
      var orderListService = serviceProvider.GetService<IOrderListService>();

      var task = new CreateOrders(orderListService,usersRepository);
      await task.InsertOrders();

      Console.WriteLine("Application finished.");
      Console.ReadLine();
   }

   static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
   {

      services.AddDbContext<ApplicationDbContext>();
      services.AddScoped<DbContext, ApplicationDbContext>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IUsersRepository, UsersRepository>();
      services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped<IOrderListRepository, OrderListRepository>();
      services.AddScoped<IOrderRepository, OrderRepository>();
      services.AddScoped<IOrderListService, OrderListService>();
      services.AddSingleton(AutoMapperConfig.Initialize());
   }
}