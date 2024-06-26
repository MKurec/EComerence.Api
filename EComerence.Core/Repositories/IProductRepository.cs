﻿using EComerence.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid id);
        Task<Product> GetAsync(string name);
        Task<IEnumerable<Product>> BrowseAsync(string name = "");
        Task<IEnumerable<Product>> BrowseAsyncInCategory(Guid categoryId);

        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task UpdateBulkAsync(IEnumerable<Product> products);
    }

   public interface IUserProductProbabilityRepository
   {
      Task<List<UserProductProbability>> GetAsync(Guid id, int num);
   }
}

