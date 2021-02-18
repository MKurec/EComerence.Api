using EComerence.Core.Domain;
using EComerence.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetAsync(Guid id);
        Task<ProductDto> GetAsync(string email);


        Task<IEnumerable<ProductDto>> BrowseAsync(string name = null);
        Task<IEnumerable<ProductDto>> BrowseAsyncInCategory(Guid categoryId);
        Task AddAsync(Guid id, string name, int amount, decimal price, string producerName, string categoryName ,string description);
        Task UpdateAsync(Guid id, string name, decimal price, int amount);
        Task DeleteAsync(Guid id);


    }
}
