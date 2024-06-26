﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EComerence.Api.Controllers;
using EComerence.Core.Domain;
using EComerence.Core.Repositories;
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
      private readonly IUserProductProbabilityRepository _userProductProbabilityRepository;

      public ProductsController(IProductService productService, IUserProductProbabilityRepository userProductProbabilityRepository, IWebHostEnvironment environment)
      {
         _productService = productService;
         _environment = environment ?? throw new ArgumentNullException(nameof(environment));
         _userProductProbabilityRepository = userProductProbabilityRepository;
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
         if(productId == Guid.Empty)
            {
            return NotFound();
         }
     
         var @product = await _productService.GetAsync(productId, UserId);
         if (@product == null)
         {
            return NotFound();
         }
         return Json(@product);
      }
      [Route("Image/{productId}")]
      [HttpGet]
      public async Task<IActionResult> Get(Guid productId, bool a)
      {
         var @image = await _productService.GetPhotoAsync(productId);
         if (@image == null)
         {
            return NotFound();
         }
         return File(@image, "image/jpeg");
      }
      [HttpGet("Category/{categoryId}")]
      public async Task<IActionResult> Get(Guid categoryId, byte b)
      {
         var @product = await _productService.BrowseAsyncInCategory(categoryId);
         if (@product == null)
         {
            return NotFound();
         }
         return Json(@product);
      }
      [HttpPost]
      public async Task<IActionResult> Post([FromBody] AddProduct command)
      {
         var Id = Guid.NewGuid();
         await _productService.AddAsync(Id, command.Name, command.Amount, command.Price, command.ProducerName, command.CategoryName, command.Description, command.BrandTag);
         return Json(Id);

      }
      [HttpPut("{productId}")]
      public async Task<IActionResult> Put(Guid productId, [FromBody] UpdateProduct command)
      {
         await _productService.UpdateAsync(productId, command.Description, command.Price, command.Amount);
         return NoContent();
      }
      [HttpPost("AddPhoto/{productId}")]
      public async Task<string> UploadFile(Guid productId, [FromHeader] FIleUploadAPI file)
      {
         if (file.photo.ToString().Length > 0)
         {
            await _productService.AddPhotoAsync(productId, file.photo);
         }
         return "0";

      }
      [HttpDelete("{productId}")]
      public async Task<IActionResult> Delete(Guid productId)
      {
         await _productService.DeleteAsync(productId);
         return NoContent();
      }
   }

}