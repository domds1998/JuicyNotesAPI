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


        [HttpGet("all")]
        public async Task<IActionResult> getAllCollections() {
            return new OkObjectResult(_services.getAllCollections());
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> getUserCollections(User user) {
            return new OkObjectResult(_services.getUserCollections(user));
        }

        [Authorize]
        [HttpGet("{id}")]
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
        public async Task<IActionResult> getCollection(CollectionAddRequest request)
        { 
            return new OkObjectResult(_services.addCollection(request, (User)HttpContext.Items["User"]));
        }

        //TODO COLLECTION UPDATE

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCollection(int idCollection) {
            var response = _services.deleteCollection(idCollection);

            if (!response) return new NotFoundResult();

            return new OkResult();
        }

        [Authorize]
        [HttpDelete("{name}")]
        public async Task<IActionResult> deleteCollection(string name) {
            var response = _services.deleteCollection(name, (User)HttpContext.Items["User"]);

            if (!response) return new NotFoundResult();

            return new OkResult();
        }
    }
}