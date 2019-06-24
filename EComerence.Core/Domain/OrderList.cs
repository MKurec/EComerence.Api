using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EComerence.Core.Domain
{
    public class OrderList :Entity
    {
        private ICollection<Order> _orders = new HashSet<Order>();

        public IEnumerable<Order> Orders => _orders;

        public Guid UserId { get; protected set; }

        public bool Purchased { get; protected set; }

        public decimal TotalPrice { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public DateTime? PucharsedAt { get; protected set; }

        protected OrderList () { }

        public OrderList(Guid id,Guid userId)
        {
            Id = id;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddOrder(Product product, int amount)
        {
            if( _orders.Where(x => x.ProductId == product.Id).Count() > 0 )
            {
                var order = _orders.SingleOrDefault(x => x.ProductId == product.Id);
                var orderAmount = order.Amount;
                order.UpdateAmmout(amount+orderAmount);
            }

            else _orders.Add(new Order(Guid.NewGuid(),this.Id, product.Id,product.Name, product.Price, amount));
        }
        public void RemoveOrder(Guid orderId)
        {
            var @order = _orders.SingleOrDefault(x => x.Id == orderId);
            _orders.Remove(order);
        }
        public void UpdateOrder(Guid orderId, int amount)
        {
            var @order = _orders.SingleOrDefault(x => x.Id == orderId);
            @order.UpdateAmmout(amount);
        }
        
    }
}
