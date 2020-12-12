using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using JuicyNotesAPI.Attributes;
using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace JuicyNotesAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserDbService _services;

        public UserController(IUserDbService services) {
            _services = services;
        }


        [HttpPost("register")]
        public async Task<IActionResult> register(RegistrationRequest request) {
            var response = _services.register(request);
            return new OkObjectResult(response);
        }


        [HttpGet("authenticate")]
        public async Task<IActionResult> authenticate(AuthenticateRequest request)
        {
            var response = _services.authenticate(request);

            if (response == null) return new NotFoundObjectResult(new {message = $"User: {request.Username} Not Found"});

            if (response.Token == null) return new UnauthorizedObjectResult(new { message = $"Cannot authenticate user: {request.Username}" });

            return new OkObjectResult(response);
        }


        //[Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> getUser(int id) {
            var response = _services.getUser(id);

            if (response == null) return new NotFoundObjectResult(new { message = "there is no such user" });

            return new OkObjectResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> getUsers() {
            return new OkObjectResult( _services.getUsers());
        }
        //[HttpDelete("deleteUser/{id}")]

        //[HttpPut("updateUser/{id}")]
    }
}