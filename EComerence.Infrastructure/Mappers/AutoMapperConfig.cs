using AutoMapper;
using EComerence.Core.Domain;
using EComerence.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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
             cfg.CreateMap<Category, CategoryTreeDto>();
             cfg.CreateMap<OrderList, OrderListDto>()
                    .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => MapOrders(src.Orders, src.Id)));
          })
          .CreateMapper();

      private static IEnumerable<OrderDto> MapOrders(Dictionary<Product, ushort> orders, Guid id)
      {
         return orders.Select(kvp => new OrderDto
         {
            OrderListId = id,
            ProductId = kvp.Key.Id,
            ProductName = kvp.Key.Name,
            Amount = kvp.Value,
            Price = kvp.Key.Price * kvp.Value
         });
      }
   }
}
