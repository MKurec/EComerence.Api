using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Infrastructure.DTO
{
    public class OrderDto
    {
        public Guid OrderListId { get;  set; }

        public Guid ProductId { get;  set; }

        public string ProductName { get;  set; }

        public int Amount { get;  set; }

        public decimal Price { get;  set; }
    }
}
