using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EComerence.Infrastructure.Commands;
using EComerence.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace EComerence.Api.Controllers
{
    public class ProducersController : DefaultController
    {
        private readonly IProducerService _producerService;
        public ProducersController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var producers = await _producerService.BrowseAsync();
            return Json(producers);
        }
        [HttpGet("{producerId}")]
        public async Task<IActionResult> Get(Guid producerId)
        {
            var @producer = await _producerService.GetAsync(producerId);
            if (@producer == null)
            {
                return NotFound();
            }
            return Json(@producer);
        }
        [HttpGet("{producerName}")]
        public async Task<IActionResult> Get(string name)
        {
            var @producer = await _producerService.GetAsync(name);
            if (@producer == null)
            {
                return NotFound();
            }
            return Json(@producer);
        }

        [HttpPut("{producerId}")]
        public async Task<IActionResult> Put(Guid producerId, [FromBody]UpdateProducer command)
        {
            await _producerService.UpdateAsync(producerId, command.Name);
            return NoContent();
        }
        [HttpDelete("{producerId}")]
        public async Task<IActionResult> Delete(Guid producerId)
        {
            await _producerService.DeleteAsync(producerId);
            return NoContent();
        }
    }
}