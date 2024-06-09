using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EComerence.Core.Domain
{
    public class Order : Entity
    {
        public Guid UserId { get; protected set; }
        public Guid ProductId { get; protected set; }
        public string ProducerName { get; protected set; }
        public int Amount { get; protected set; }
        public decimal Price { get; protected set; }
        public BrandTags BrandTag { get; protected set; }
        public bool Bought { get; protected set; }

        protected Order() { }

        public Order(Guid userid, Guid productId, string productName, decimal price, int amount, BrandTags brandTag)
        {
            UserId = userid;
            ProductId = productId;
            ProducerName = productName;
            Amount = amount;
            Price = price;
            BrandTag = brandTag;
            Bought = true;
        }
    }
}
