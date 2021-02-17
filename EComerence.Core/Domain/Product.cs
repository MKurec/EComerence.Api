using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Core.Domain
{
    public class Product :Entity
    {
        public string Name { get; protected set; }
        public int Amount { get; protected set; }
        public decimal Price { get; protected set; }
        public Guid ProducerId { get; protected set; }
        public Guid CategoryId { get; protected set; }
        public string Description { get; protected set; }
        public string ProducerName { get; protected set; }
        public string CategoryName { get; protected set; }

        public Product(Guid id,string name, int amount, decimal price,string producerName  ,Guid producerId, string categoryName, Guid categoryId, string description)
        {
            Id = id;
            SetName(name);
            SetAmount(amount);
            SetPrice(price);
            SetDescription(description);
            ProducerId = producerId;
            ProducerName = producerName;
            CategoryId = categoryId;
            CategoryName = categoryName;
            
        }
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception($"Product with id : '{Id}' can not have empty name");
            }
            Name = name;
        }
        public void SetDescription(string description)
        {
            //if (string.IsNullOrEmpty(description))
            //{
            //    throw new Exception($"Product with id : '{Id}' can not have empty description");
            //}
            Description = description;
        }
        public void SetAmount(int amount)
        {
            Amount= amount;
        }
        public void SetPrice(decimal price)
        {
            if (price <= 0)
            {
                throw new Exception($"Product with id : '{Id}' can not have price set <=0 ");
            }
            Price = price;
        }



    }
}
