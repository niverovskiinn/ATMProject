using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engine.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Engine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountsService _accountsService;
// PUT    api/users?id=5&team=2
//        PutUserTeam(int id, int team, [FromBody] User value)

        // GET api/values
        
        public AccountsController(AccountsService accountsService)
        {
            _accountsService = accountsService;
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
            return "value" + r;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Account account)
        {
            
            
//            try
//            {
//                await _accountsService.(user);
                return Ok();
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, "Internal server error\n" + ex);
//            }
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