using EComerence.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetAsync(Guid id);
        Task<CategoryDto> GetAsync(string name);

        Task<IEnumerable<CategoryDto>> BrowseAsync(string name = null);
        Task<IEnumerable<CategoryTreeDto>> CategoryTreeAsync(string name = null);
        Task UpdateAsync(Guid id, string name);
        Task DeleteAsync(Guid id);
        Task AddSubCategory(Guid categoryId, Guid subCategoryId, string name);
        Task CreateAsync(Guid id, string name);
    }
}
