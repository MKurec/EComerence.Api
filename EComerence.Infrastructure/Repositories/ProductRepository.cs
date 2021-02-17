using EComerence.Core.Domain;
using EComerence.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Repositories
{
    public class ProductRepository :IProductRepository
    {
        protected readonly DbContext Context;
        private DbSet<Product> products;
        public ProductRepository(DbContext context)
        {
            this.Context = context;
            this.products = Context.Set<Product>();
        }



        public async Task<Product> GetAsync(Guid id)
        {
            var @product = await Task.FromResult(Context.Set<Product>().SingleOrDefault(x => x.Id == id));
            return @product;
        }
        public async Task<Product> GetAsync(string name)
        {
            var @product = await Task.FromResult(Context.Set<Product>().SingleOrDefault(x => x.Name.ToLower() == name.ToLower()));
            return @product;
        }

        public async Task AddAsync(Product @product)
        {
            products.Add(@product);
            Context.SaveChanges();
            await Task.CompletedTask;
        }
        public async Task UpdateAsync(Product @product)
        {
            Context.Entry(@product).State = EntityState.Modified;
            Context.SaveChanges();
            await Task.CompletedTask;

        }
        public async Task DeleteAsync(Product @product)
        {
            products.Remove(@product);
            Context.SaveChanges();
            await Task.CompletedTask;

        }



        public async Task<IEnumerable<Product>> BrowseAsync(string name = "")
        {
            var xproducts = products.AsEnumerable();
            if (!string.IsNullOrEmpty(name))
            {
                xproducts = xproducts.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            return await Task.FromResult(xproducts);
        }
    }
}
