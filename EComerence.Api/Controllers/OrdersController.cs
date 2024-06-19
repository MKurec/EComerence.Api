using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EComerence.Infrastructure.Commands;
using EComerence.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EComerence.Api.Controllers
{
   public class OrdersController : DefaultController
   {
      private readonly IOrderListService _orderListService;
      public OrdersController(IOrderListService orderListService)
      {
         _orderListService = orderListService;

      }
      [Authorize]
      [HttpGet]
      public async Task<IActionResult> Get()
      {
         var orders = await _orderListService.GetCurrentAsync(UserId);
         if (orders == null)
         {
            return NotFound();
         }
         return Json(orders);
      }

      [Authorize]
      [HttpGet("ItemsCount")]
      public async Task<IActionResult> GetItemsCount()
      {
         if (UserId == Guid.Empty)
         {
            return Json(0);
         }
         var orders = await _orderListService.GetCurrentCountItemsAsync(UserId);
         return Json(orders);
      }
      [Authorize]
      [HttpPost("SubmitOrder")]
      public async Task<IActionResult> SumbitOrder()
      {
         try
         {
            await _orderListService.SubmitOrder(UserId);
            return Ok("Order submitted successfully");
         }
         catch (Exception ex)
         {
            return StatusCode(500, "An error occurred while submitting the order");
         }
      }
      [Authorize]
      [HttpGet("OrderList/{orderListId}")]
      public async Task<IActionResult> Get(Guid orderListId)
      {
         var @orderList = await _orderListService.GetAsync(orderListId);
         if (@orderList == null)
         {
            return NotFound();
         }
         return Json(@orderList);
      }
      [Authorize]
      [HttpPost]
      public async Task<IActionResult> Post([FromBody] AddOrder command)
      {
         await _orderListService.AddOrderAsync(UserId, command.ProductId, command.Amount);
         return Created($"/orders/", null);

      }
      [Authorize]
      [HttpDelete("{orderListId}")]
      public async Task<IActionResult> Delete(Guid orderListId)
      {
         await _orderListService.DeleteAsync(orderListId);
         return NoContent();
      }
      [Authorize]
      [HttpDelete("{orderListId}/{orderId}")]
      public async Task<IActionResult> Delete(Guid orderListId, Guid orderId)
      {
         await _orderListService.DeleteOrderAsync(orderListId, orderId);
         return NoContent();
      }
   }
}