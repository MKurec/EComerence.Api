using EComerence.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> BrowseAsync(string name = "");
        Task<Category> GetAsync(string name);
        Task<Category> GetAsync(Guid id);

        Task AddAsync(Category category);
        Task UpdateAsync(Category product);
        Task DeleteAsync(Category product);
    }
}
