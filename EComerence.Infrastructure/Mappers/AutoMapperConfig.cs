using AutoMapper;
using EComerence.Core.Domain;
using EComerence.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<User, AccountDto>();
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<Producer, ProducerDto>();
                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<OrderList, OrderListDto>();
                cfg.CreateMap<Order, OrderDto>();
            })
            .CreateMapper();
    }
}
