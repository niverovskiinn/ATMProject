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

        public AccountsController(AccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        
        
        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Account>> Post([FromBody] dynamic data)
        {
            
            
            try
            {
               var acc = await _accountsService.GetAccAsync(data);
                return Ok(acc);
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