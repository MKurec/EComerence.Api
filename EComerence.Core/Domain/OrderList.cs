using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EComerence.Core.Domain
{
    public class OrderList : Entity
    {
        private ICollection<Order> _orders = new HashSet<Order>();

        public IEnumerable<Order> Orders => _orders;

        public Guid UserId { get; protected set; }

        public bool Purchased { get; protected set; }

        public decimal TotalPrice { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public DateTime? PucharsedAt { get; protected set; }

        protected OrderList() { }

        public OrderList(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
            SetTotalPrice();

        }
        public void RemoveOrder(Guid orderId)
        {
            var @order = _orders.SingleOrDefault(x => x.Id == orderId);
            _orders.Remove(order);
            SetTotalPrice();

        }
        public void UpdateOrder(Guid orderId, int amount)
        {
            var @order = _orders.SingleOrDefault(x => x.Id == orderId);
            @order.UpdateAmmout(amount);
            SetTotalPrice();
        }
        public void SetTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (Order order in Orders)
            {
                totalPrice += order.Price;
            }
            TotalPrice = totalPrice;
        }

    }
}
