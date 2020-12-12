using System.Threading.Tasks;
using JuicyNotesAPI.DTOs.Requests;
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


        [HttpGet("user/{id}")]
        public async Task<IActionResult> getUserCollections(int idUser) {
            return new OkObjectResult(_services.getUserCollections(idUser));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> getCollection(int idCollection) {
            var collection = _services.getCollection(idCollection);

            if (collection == null) return new NotFoundResult();

            return new OkObjectResult(collection);
        }


        [HttpGet("{name}")]
        public async Task<IActionResult> getCollection(string name) {
            var collection = _services.getCollection(name);

            if (collection == null) return new NotFoundResult();

            return new OkObjectResult(collection);
        }


        [HttpPost("add")]
        public async Task<IActionResult> getCollection(CollectionAddRequest request)
        {
         
            return new OkObjectResult(null);
        }

        //TODO COLLECTION UPDATE


        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCollection(int idCollection) {
            var response = _services.deleteCollection(idCollection);

            if (!response) return new NotFoundResult();

            return new OkResult();
        }


        [HttpDelete("{name}")]
        public async Task<IActionResult> deleteCollection(string name) {
            var response = _services.deleteCollection(name);

            if (!response) return new NotFoundResult();

            return new OkResult();
        }
    }
}