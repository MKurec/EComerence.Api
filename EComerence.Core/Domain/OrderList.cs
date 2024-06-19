using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EComerence.Core.Domain
{
   public class OrderList : Entity
   {
      public Dictionary<Product, ushort> Orders = new();

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

      public void AddProduct(Product product, ushort ammount)
      {
         if (ContainsProduct(product))
            UpdateOrder(product, ammount);
         else
         {
            Orders.Add(product, ammount);
            SetTotalPrice();
         }

      }
      public void RemoveProduct(Guid productId)
      {
         if (!Orders.Keys.Any(x => x.Id == productId))
            return;
         var productToRemove = Orders.Keys.FirstOrDefault(x => x.Id == productId);
         Orders.Remove(productToRemove);
         SetTotalPrice();

      }
      public void UpdateOrder(Product product, ushort amount)
      {
         if (!ContainsProduct(product))
            AddProduct(product, amount);
         RemoveProduct(product.Id);
         AddProduct(product, amount);
         SetTotalPrice();
      }
      public bool ContainsProduct(Product product)
      {
         return Orders.Keys.Contains(product);
      }

      public void SetTotalPrice()
      {
         decimal totalPrice = 0;
         foreach (Product product in Orders.Keys)
         {
            totalPrice += product.Price * Orders[product];
         }
         TotalPrice = totalPrice;
      }

      public void PucharseProducts()
      {
         PucharsedAt = DateTime.UtcNow;
         Purchased = true;
      }

      public int GetAmountOfProducts()
      {
         if (Orders.Count == 0)
            return 0;
         else
         {
            int amount = 0;
            foreach (Product product in Orders.Keys)
            {
               amount += Orders[product];
            }
            return amount;
         }
      }

   }
}
