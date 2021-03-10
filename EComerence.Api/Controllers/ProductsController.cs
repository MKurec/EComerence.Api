using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EComerence.Api.Controllers;
using EComerence.Core.Domain;
using EComerence.Infrastructure.Commands;
using EComerence.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EComerence.Api.Controllers
{
    public class ProductsController : DefaultController
    {
        private readonly IWebHostEnvironment _environment; 
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, IWebHostEnvironment environment)
        {
            _productService = productService;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }
        public class FIleUploadAPI
        {
            public IFormFile photo { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.BrowseAsync();
            return Json(products);
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(Guid productId)
        {
            var @product = await _productService.GetAsync(productId);
            if (@product == null)
            {
                return NotFound();
            }
            return Json(@product);
        }
        [HttpGet("{Category}/{categoryId}")]
        public async Task<IActionResult> Get(Guid categoryId, string Category)
        {
            var @product = await _productService.BrowseAsyncInCategory(categoryId);
            if (@product == null || Category != "Category")
            {
                return NotFound();
            }
            return Json(@product);
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile([FromBody] AddProduct command)
        {
            var Id = Guid.NewGuid();
            await _productService.AddAsync(Id, command.Name, command.Amount, command.Price, command.ProducerName, command.CategoryName,command.Description,command.BrandTag);
            return Created($"/product/{Id}", null);

        }
        [HttpPut("{productId}")]
        public async Task<IActionResult> Put(Guid productId, [FromBody]UpdateProduct command)
        {
            await _productService.UpdateAsync(productId, command.Description, command.Price,command.Amount);
            return NoContent();
        }
        [HttpPut("AddPhoto/{productId}")]
        public async Task<IActionResult> Put(Guid productId, FIleUploadAPI file)
        {
            if(file.photo.Length > 0)
            {
                string path = _environment.WebRootPath.ToString();
                await _productService.AddPhotoAsync(path, productId, file.photo);
            }
            return NoContent();

        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(Guid productId)
        {
            await _productService.DeleteAsync(productId);
            return NoContent();
        }
    }

}