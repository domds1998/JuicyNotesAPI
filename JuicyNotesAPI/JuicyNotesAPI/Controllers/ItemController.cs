using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuicyNotesAPI.Attributes;
using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JuicyNotesAPI.Controllers
{

    [Route("api/item")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly IItemDbService _services;


        public ItemController(IItemDbService services)
        {
            _services = services;
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteItem(int id)
        {
            var deletedItem = _services.deleteItem(id);

            if (deletedItem == null) return new BadRequestResult();

            return new OkObjectResult(deletedItem);

        }
        [Authorize]
        [HttpPost("{id}")]
        public async Task<IActionResult> addItem(AddingItemRequest request)
        {
            var addedItem = _services.addItem(request);

        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> updateItem(UpdateItemRequest request)
        {

        }

    }
}
