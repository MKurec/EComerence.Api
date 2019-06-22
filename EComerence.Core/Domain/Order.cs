using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Core.Domain
{
    public class Order : Entity
    {
        public Guid UserId { get; protected set; }
        public Guid ProductId { get; protected set; }
        public string ProductName { get; protected set; }
        public int Amount { get; protected set; }
        public decimal Price { get; protected set; }

        public Order(Guid userId,Guid productId,string productName, decimal price  , int amount)
        {
            UserId = userId;
            ProductId = productId;
            ProductName = productName;
            Amount = amount;
            Price = Convert.ToDecimal(price);
        }
    }
}
