using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JuicyNotesAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> register() {
            return new OkObjectResult(null);
        }


        /*[HttpGet("authenticate")]

        [HttpGet("getUsers")]

        [HttpGet("getUser/{username}")]

        [HttpGet("getUser/{id}")]

        [HttpGet("getUser/{mail}")]

        [HttpDelete("deleteUser/{id}")]

        [HttpPut("updateUser/{id}")]*/
    }
}