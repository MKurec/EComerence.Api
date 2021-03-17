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
            var orders = await _orderListService.BrowseAsync(UserId);
            if (orders == null)
            {
                return NotFound();
            }
            return Json(orders);
        }
        [Authorize]
        [HttpGet("{orderListId}")]
        public async Task<IActionResult> Get(Guid orderListId)
        {
            var @order = await _orderListService.BrowseOrdersAsync(orderListId);
            if (@order == null)
            {
                return NotFound();
            }
            return Json(@order);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddOrder command)
        {
            var Id = Guid.NewGuid();
            await _orderListService.AddOrderAsync(Id,UserId,command.ProductId,command.Amount );
            return Created($"/orders/{Id}", null);

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