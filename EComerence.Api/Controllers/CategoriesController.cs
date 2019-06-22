using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EComerence.Infrastructure.Commands;
using EComerence.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace EComerence.Api.Controllers
{
    public class CategoriesController : DefaultController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var categories = await _categoryService.BrowseAsync();
            return Json(categories);
        }
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> Get(Guid categoryId)
        {
            var @category = await _categoryService.GetAsync(categoryId);
            if (@category == null)
            {
                return NotFound();
            }
            return Json(@category);
        }
        [HttpGet("{categoryName}")]
        public async Task<IActionResult> Get(string name)
        {
            var @category = await _categoryService.GetAsync(name);
            if (@category == null)
            {
                return NotFound();
            }
            return Json(@category);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Put(Guid categoryId, [FromBody]UpdateProducer command)
        {
            await _categoryService.UpdateAsync(categoryId, command.Name);
            return NoContent();
        }
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            await _categoryService.DeleteAsync(categoryId);
            return NoContent();
        }
    }
}