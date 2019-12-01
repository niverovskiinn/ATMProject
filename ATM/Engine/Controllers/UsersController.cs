using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engine.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Engine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }
      
        // POST api/users/login
        [HttpPost("login")]
        public async Task<ActionResult<User>> Post([FromBody] dynamic data)
        {
            try
            {
                var user = await _usersService.LoginToAtm(data);
                return Ok(user) ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}