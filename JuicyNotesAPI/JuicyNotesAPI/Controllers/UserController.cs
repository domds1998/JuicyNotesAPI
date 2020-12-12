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
        public async Task<IActionResult> register() {
            return new OkObjectResult(null);
        }


        [HttpGet("authenticate")]
        public async Task<IActionResult> authenticate(AuthenticateRequest request)
        {
            return new OkObjectResult(null);
        }


        [Authorize]
        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> getUser(int id) {
            return new OkObjectResult(null);
        }

        //[HttpDelete("deleteUser/{id}")]

        //[HttpPut("updateUser/{id}")]
    }
}