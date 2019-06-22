using AutoMapper;
using EComerence.Core.Repositories;
using EComerence.Infrastructure.DTO;
using EComerence.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Services
{
    public class CategoryService :ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository=categoryRepository;
        }
        public async Task<CategoryDto> GetAsync(Guid id)
        {
            var @category = await _categoryRepository.GetAsync(id);
            return _mapper.Map<CategoryDto>(@category);
        }
        public async Task<CategoryDto> GetAsync(string name)
        {
            var @category = await _categoryRepository.GetAsync(name);
            return _mapper.Map<CategoryDto>(@category);

        }
        public async Task<IEnumerable<CategoryDto>> BrowseAsync(string name = null)
        {
            var categories = await _categoryRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }


        public async Task UpdateAsync(Guid id, string name)
        {
            var @category = await _categoryRepository.GetOrFailAsync(id);
            @category.SetName(name);
            await _categoryRepository.UpdateAsync(@category);
        }
        public async Task DeleteAsync(Guid id)
        {
            var @category = await _categoryRepository.GetOrFailAsync(id);
            await _categoryRepository.DeleteAsync(@category);
        }
    }
}
