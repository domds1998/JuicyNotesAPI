using System.Threading.Tasks;
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
            return new OkObjectResult(null);
        }


        [HttpGet("user/{id}")]
        public async Task<IActionResult> getUserCollections(int idUser) {
            return new OkObjectResult(null);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> getCollection(int idCollection) {
            return new OkObjectResult(null);
        }


        [HttpGet("{name}")]
        public async Task<IActionResult> getCollection(string name) {
            return new OkObjectResult(null);
        }


        //TODO COLLECTION UPDATE


        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCollection(int idCollection) {
            return new OkObjectResult(null);
        }


        [HttpDelete("{name}")]
        public async Task<IActionResult> deleteCollection(string name) {
            return new OkObjectResult(null);
        }
    }
}