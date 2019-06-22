using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Core.Domain
{
    public class OrderList :Entity
    {
        private ICollection<Order> _orders = new HashSet<Order>();

        public IEnumerable<Order> Orders => _orders;

        public bool Purchased { get; protected set; }

        public decimal TotalPrice { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public DateTime? PucharsedAt { get; protected set; }

        public OrderList(Guid id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddOrder(Product product, int amount)
        {
            _orders.Add(new Order(this.Id, product.Id,product.Name, product.Price, amount));
        }
    }
}
