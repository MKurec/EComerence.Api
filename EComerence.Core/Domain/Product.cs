using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EComerence.Core.Domain
{
   public class Product : Entity
   {
      public string Name { get; protected set; }
      public int Amount { get; protected set; }
      public decimal Price { get; protected set; }
      public Guid ProducerId { get; protected set; }
      public Guid CategoryId { get; protected set; }
      public BrandTags BrandTag { get; protected set; }
      public string Description { get; protected set; }
      public string ProducerName { get; protected set; }
      public string CategoryName { get; protected set; }
      public Guid? CopurchasedProductId { get; protected set; }

      public string RecomendationsJson { get; protected set; }

      public virtual List<UserProductProbability> UserProductProbabilities { get; set; }

      public Product(Guid id, string name, int amount, decimal price, string producerName, Guid producerId, string categoryName, Guid categoryId, string description, string brandTag)
      {
         Id = id;
         SetName(name);
         SetAmount(amount);
         SetPrice(price);
         SetDescription(description);
         SetBrandTag(brandTag);
         ProducerId = producerId;
         ProducerName = producerName;
         CategoryId = categoryId;
         CategoryName = categoryName;
      }
      [JsonConstructor]

      public Product(Guid id, string name, int amount, decimal price, string producerName, Guid producerId, string categoryName, Guid categoryId, string description, BrandTags brandTag)
      {
         Id = id;
         SetName(name);
         SetAmount(amount);
         SetPrice(price);
         SetDescription(description);
         BrandTag = brandTag;
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
         if (string.IsNullOrEmpty(description))
         {
            throw new Exception($"Product with id : '{Id}' can not have empty description");
         }
         Description = description;
      }
      public void SetAmount(int amount)
      {
         Amount = amount;
      }
      public void SetPrice(decimal price)
      {
         if (price <= 0)
         {
            throw new Exception($"Product with id : '{Id}' can not have price set <=0 ");
         }
         Price = price;
      }
      public void SetBrandTag(string brandTag)
      {
         if (string.IsNullOrWhiteSpace(brandTag))
         {
            throw new Exception("Product cannot have an empty brand tag.");
         }

         brandTag = brandTag.ToLowerInvariant();

         if (!Enum.TryParse<BrandTags>(brandTag, true, out var selectedBrandTag))
         {
            throw new Exception($"Invalid brand tag: {brandTag}.");
         }

         BrandTag = selectedBrandTag;
      }
      public void SetCopurchasedProductId(Guid id)
      {
         CopurchasedProductId = id;
      }
      public void SetRecomendations(Dictionary<Guid,float> dictionary)
      {
         RecomendationsJson = System.Text.Json.JsonSerializer.Serialize(dictionary);
      }
      public Dictionary<Guid, float> GetRecomendations()
      {
         return string.IsNullOrEmpty(RecomendationsJson)
            ? new Dictionary<Guid, float> ()
            : System.Text.Json.JsonSerializer.Deserialize<Dictionary<Guid, float>>(RecomendationsJson);
      }


   }
   public enum BrandTags
   {
      premium,
      standard,
      budget,
   }
}
