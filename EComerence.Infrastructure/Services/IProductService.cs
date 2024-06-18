using EComerence.Core.Domain;
using EComerence.Infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetAsync(Guid id, Guid userId);
        Task<ProductDto> GetAsync(string email);
        Task<FileStream> GetPhotoAsync(Guid id);


        Task<IEnumerable<ProductDto>> BrowseAsync(string name = null);
        Task<IEnumerable<ProductDto>> BrowseAsyncInCategory(Guid categoryId);
        Task AddAsync(Guid id, string name, int amount, decimal price, string producerName, string categoryName, string brandTag, string descryption);
        Task UpdateAsync(Guid id, string name, decimal price, int amount);
        Task AddPhotoAsync(Guid id, IFormFile photo);
        Task DeleteAsync(Guid id);


    }
}
