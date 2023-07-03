using EComerence.Core.Repositories;
using EComerence.Core;
using EComerence.Infrastructure.Mappers;
using EComerence.Infrastructure.Repositories;
using EComerence.Infrastructure.Services;
using MachineLearningWorker;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
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
        services.AddSingleton(AutoMapperConfig.Initialize());
    })
    .Build();

host.Run();
