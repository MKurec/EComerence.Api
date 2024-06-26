﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Infrastructure.DTO
{
   public class ProductDto
   {
      public Guid Id { get; set; }
      public string Name { get; set; }
      public int Amount { get; set; }
      public decimal Price { get; set; }
      public string ProducerName { get; set; }
      public string CategoryName { get; set; }
      public string Description { get; set; }
      public string BrandTag { get; set; }
      public List<Guid> CopurchasedProductIds { get; set; }

   }
}
