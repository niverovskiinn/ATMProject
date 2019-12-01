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
        
        
        // POST api/accounts/by_id
        // get account 
        // need "id" account
        [HttpPost("by_id")]
        public async Task<ActionResult<Account>> PostGetAccount([FromBody] dynamic data)
        {
            try
            {
               var acc = await _accountsService.GetAccAsync(data);
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/accounts/list
        // get accounts 
        // need "passport" user
        [HttpPost("list")]
        public async Task<ActionResult<IEnumerable<Account>>> PostGetAccountsList([FromBody] dynamic data)
        {
            try
            {
                var acc = await _accountsService.GetUserAccountsAsync(data);
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/accounts/froze
        // froze account 
        // need "id" account
        [HttpPost("froze")]
        public async Task<ActionResult> PostSetAccFrozen([FromBody] dynamic data)
        {
            try
            {
                await _accountsService.SetAccFrozen(data);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}