using System.Threading.Tasks;
using JuicyNotesAPI.Attributes;
using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using JuicyNotesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JuicyNotesAPI.Controllers
{
    [Route("api/collection")]
    [ApiController]
    public class CollectionController : ControllerBase
    {

        private readonly ICollectionDbService _services;

        public CollectionController(ICollectionDbService services) {
            _services = services;
        }


        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> getUserCollections() {
            return new OkObjectResult(await _services.getUserCollections((User)HttpContext.Items["User"]));
        }

        [Authorize]
        [HttpGet("{idCollection}")]
        public async Task<IActionResult> getCollection(int idCollection) {
            return await _services.getCollection(idCollection);
        }

        [Authorize]
        [HttpGet("{name}")]
        public async Task<IActionResult> getCollection(string name) {
            return await _services.getCollection(name, (User)HttpContext.Items["User"]); ;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> addCollection(CollectionAddRequest request){
            return await _services.addCollection(request, (User)HttpContext.Items["User"]);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> updateCollection(CollectionUpdateRequest request) {
            return await _services.updateCollection(request, (User)HttpContext.Items["User"]);
        } 

        [Authorize]
        [HttpDelete("{idCollection}")]
        public async Task<IActionResult> deleteCollection(int idCollection) {
            return await _services.deleteCollection(idCollection);
        }

    }
}