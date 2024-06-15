using EComerence.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace EComerence.Infrastructure.Repositories
{
   public class ApplicationDbContext : DbContext
   {
      private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
      {
         DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
         Converters = {
                new DictionaryJsonConverter<Product, ushort>(),
                new JsonStringEnumConverter()
            }
      };
      const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EComerence;Trusted_Connection=True;";

      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
      {
      }


      public DbSet<Order> Orders { get; set; }
      public DbSet<OrderList> OrderLists { get; set; }
      public DbSet<Product> Products { get; set; }
      public DbSet<User> Users { get; set; }
      public DbSet<Producer> Producers { get; set; }
      public DbSet<Category> Categories { get; set; }
      public DbSet<UserProductProbability> UserProductProbabilitys { get; set; }





      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("EComerence.Api"));
      }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {

         modelBuilder.Entity<Category>(entity =>
         {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name);
            entity.HasMany(x => x.SubCategories)
                     .WithOne(e => e.Parent)
                     .HasForeignKey(x => x.ParentId)
                     .IsRequired(false)
                     .OnDelete(DeleteBehavior.Restrict);
         });
         modelBuilder.Entity<OrderList>()
                    .Property(o => o.Orders)
                    .HasConversion(
                    d => JsonSerializer.Serialize(d, JsonOptions),
                    s => JsonSerializer.Deserialize<Dictionary<Product, ushort>>(s, JsonOptions));
         modelBuilder.Entity<Product>()
                     .Property(p => p.BrandTag)
                     .HasConversion<string>()
                     .IsRequired(true);
         modelBuilder.Entity<UserProductProbability>(entity =>
         {
            entity.HasKey("ProductId", "UserId");
            entity.HasOne(p => p.Product)
            .WithMany(p => p.UserProductProbabilities)
            .HasForeignKey(p => p.ProductId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.User)
            .WithMany(u => u.UserProductProbabilities)
            .HasForeignKey(u => u.ProductId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);
         });

      }
   }

   internal sealed class DictionaryJsonConverter<TKey, TValue> : JsonConverter<Dictionary<Product, ushort>>
   {
      public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(Dictionary<TKey, TValue>);

      public override Dictionary<Product, ushort> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      {
         var dictionary = new Dictionary<Product, ushort>();

         while (reader.Read())
         {
            if (reader.TokenType == JsonTokenType.EndArray)
               break;

            if (reader.TokenType != JsonTokenType.StartObject)
               throw new JsonException("Invalid JSON format.");

            Product product = null;
            ushort amount = 0;

            while (reader.Read())
            {
               if (reader.TokenType == JsonTokenType.EndObject)
                  break;

               if (reader.TokenType != JsonTokenType.PropertyName)
                  throw new JsonException("Invalid JSON format.");

               string propertyName = reader.GetString();
               reader.Read();

               if (propertyName.Equals("Key", StringComparison.OrdinalIgnoreCase))
               {
                  product = JsonSerializer.Deserialize<Product>(ref reader, options);
               }
               else if (propertyName.Equals("Value", StringComparison.OrdinalIgnoreCase))
               {
                  amount = reader.GetUInt16();
               }
            }

            if (product != null)
            {
               dictionary.Add(product, amount);
            }
         }

         return dictionary;
      }

      public override void Write(Utf8JsonWriter writer, Dictionary<Product, ushort> dictionary, JsonSerializerOptions options)
         => JsonSerializer.Serialize(writer, dictionary.ToArray(), options);


   }
}
