using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engine.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enum;

namespace Engine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;
// PUT    api/users?id=5&team=2
//        PutUserTeam(int id, int team, [FromBody] User value)

        // GET api/values
        
        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id, int r)
        {
            var l = (AccountTypeEnum) id;
            return "value" + r;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] dynamic data)
        {
            try
            {
                return Ok(_usersService.LoginToAtm(data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error\n" + ex);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}