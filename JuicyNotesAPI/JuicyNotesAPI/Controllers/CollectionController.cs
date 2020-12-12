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
            return new OkObjectResult(_services.getUserCollections((User)HttpContext.Items["User"]));
        }

        [Authorize]
        [HttpGet("{idCollection}")]
        public async Task<IActionResult> getCollection(int idCollection) {
            var collection = _services.getCollection(idCollection);

            if (collection == null) return new NotFoundResult();

            return new OkObjectResult(collection);
        }

        [Authorize]
        [HttpGet("{name}")]
        public async Task<IActionResult> getCollection(string name) {
            var collection = _services.getCollection(name, (User)HttpContext.Items["User"]);

            if (collection == null) return new NotFoundResult();

            return new OkObjectResult(collection);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> addCollection(CollectionAddRequest request)
        {
            var response = _services.addCollection(request, (User)HttpContext.Items["User"]);

            if (response == null) return new BadRequestResult();

            return new OkObjectResult(response);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> updateCollection(CollectionUpdateRequest request) {
            var response = _services.updateCollection(request, (User)HttpContext.Items["User"]);

            if (response == null) return new BadRequestResult();

            return new OkObjectResult(response);
        } 

        [Authorize]
        [HttpDelete("{idCollection}")]
        public async Task<IActionResult> deleteCollection(int idCollection) {
            var response = _services.deleteCollection(idCollection);

            if (!response) return new NotFoundResult();

            return new OkResult();
        }

    }
}