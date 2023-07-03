using EComerence.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Infrastructure.DTO
{
    public class OrderListDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get;  set; }

        public bool Purchased { get;  set; }

        public decimal TotalPrice { get;  set; }

        public DateTime CreatedAt { get;  set; }

        public DateTime? PucharsedAt { get;  set; }

        public IEnumerable<OrderDto> Orders;
    }
}
