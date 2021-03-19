using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EComerence.Core.Domain
{
    public class Order : Entity
    {
        [ForeignKey("OrderListId")]
        public Guid OrderListId { get; protected set; }
        public OrderList OrderList { get; protected set; }
        public Guid ProductId { get; protected set; }
        public string ProductName { get; protected set; }
        public int Amount { get; protected set; }
        public decimal ProductPrice { get; protected set; }
        public decimal Price { get; protected set; }

        protected Order() { }

        public Order(Guid id, OrderList orderList,Guid productId,string productName, decimal price  , int amount)
        {
            Id = id;
            OrderListId = orderList.Id;
            OrderList = orderList;
            ProductId = productId;
            ProductName = productName;
            Amount = amount;
            ProductPrice = Convert.ToDecimal(price);
            Price = Convert.ToDecimal(price* amount);
        }
        public void UpdateAmmout(int amount)
        {
            Price = Convert.ToDecimal(ProductPrice * amount);
            Amount = amount;
        }
    }
}
