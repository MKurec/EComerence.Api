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
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly DbContext Context;
        private DbSet<Category> categories;
        public CategoryRepository(DbContext context)
        {
            this.Context = context;
            this.categories = Context.Set<Category>();
        }

        public async Task AddAsync(Category @category)
        {
            categories.Add(@category);
            Context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Category>> BrowseAsync(string name = "")
        {
            var xcategories = categories.AsEnumerable();
            if (!string.IsNullOrEmpty(name))
            {
                xcategories = xcategories.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            return await Task.FromResult(xcategories);
        }

        public async Task DeleteAsync(Category @category)
        {
            categories.Remove(@category);
            Context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Category @category)
        {
            Context.Entry(@category).State = EntityState.Modified;
            Context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task<Category> GetAsync(string name)
        {
            var @category = await Task.FromResult(Context.Set<Category>().SingleOrDefault(x => x.Name == name));
            return @category;
        }
        public async Task<Category> GetAsync(Guid id)
        {
            var @category = await Task.FromResult(Context.Set<Category>().SingleOrDefault(x => x.Id == id));
            return @category;
        }
    }
}
