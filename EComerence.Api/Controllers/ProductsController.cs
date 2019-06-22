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
using Microsoft.AspNetCore.Mvc;

namespace EComerence.Api.Controllers
{
    public class ProductsController : DefaultController
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
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
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddProduct command)
        {
            var Id = Guid.NewGuid();
            await _productService.AddAsync(Id, command.Name, command.Amount, command.Price, command.ProducerName, command.CategoryName);
            return Created($"/product/{Id}", null);

        }
        [HttpPut("{productId}")]
        public async Task<IActionResult> Put(Guid productId, [FromBody]UpdateProduct command)
        {
            await _productService.UpdateAsync(productId, command.Name,command.Price,command.Amount);
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