using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using EComerence.Core.Domain;
using EComerence.Core.Repositories;
using EComerence.Infrastructure.DTO;
using EComerence.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IProducerRepository producerRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _producerRepository = producerRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<ProductDto> GetAsync(Guid id)
        {
            var @product = await _productRepository.GetAsync(id);
            return _mapper.Map<ProductDto>(@product);
        }
        public async Task<ProductDto> GetAsync(string name)
        {
            var @product = await _productRepository.GetAsync(name);
            return _mapper.Map<ProductDto>(@product);

        }
        public async Task<IEnumerable<ProductDto>> BrowseAsync(string name = null)
        {
            var products = await _productRepository.BrowseAsync(name);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }


        public async Task AddAsync(Guid id, string name, int amount, decimal price, string producerName, string categoryName, string descryption)
        {
            var @product = await _productRepository.GetAsync(name);
            var @producer = await _producerRepository.SetOrGetExistingAsync(producerName);
            var @category = await _categoryRepository.GetAsync(categoryName);
            if (@category == null) throw new Exception($"Cannot find category with '{categoryName}' ");
            @product = new Product(id, name, amount, price, @producer.Name, @producer.Id, @category.Name, @category.Id,descryption);
            await _productRepository.AddAsync(@product);
        }
        public async Task UpdateAsync(Guid id, string description, decimal price, int amount)
        {
            var @product = await _productRepository.GetOrFailAsync(id);
            @product.SetDescription(description);
            @product.SetAmount(amount);
            @product.SetPrice(price);
            await _productRepository.UpdateAsync(@product);
        }
        public async Task DeleteAsync(Guid id)
        {
            var @product = await _productRepository.GetOrFailAsync(id);
            await _productRepository.DeleteAsync(@product);
        }

    }
}
