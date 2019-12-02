using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engine.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Engine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TransactionsController : ControllerBase
    {
        private readonly TransactionsService _transactionsService;


        public TransactionsController(TransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }
        
        // POST api/transactions/user
        // get all transaction 
        // need "passport" user
        [HttpPost("user")]
        public async Task<ActionResult<IEnumerable<Transaction>>> PostGetAllUserTrans([FromBody] dynamic data)
        {
            try
            {
                var acc = await _transactionsService.GetUserAllTransAsync(data);
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return StatusCode(215, ex.Message);
            }
        }
        
        // POST api/transactions/period
        // get period transaction 
        // need  id of "account" , date "from" and date "to"
        [HttpPost("period")]
        public async Task<ActionResult<IEnumerable<Transaction>>> PostGetPeriodUserTrans([FromBody] dynamic data)
        {
            try
            {
                var acc = await _transactionsService.GetUserTransPeriodAsync(data);
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return StatusCode(215, ex.Message);
            }
        }
        
        // POST api/transactions/deposit
        // deposit your money to account  
        // need id of "account" and money "amount"
        [HttpPost("deposit")]
        public async Task<ActionResult<string>> PostDeposit([FromBody] dynamic data)
        {
            try
            {
                var acc = await _transactionsService.DepositMoney(data);
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return StatusCode(215, ex.Message);
            }
        }
        
        // POST api/transactions/withdraw
        // withdraw your money to account  
        // need id of "account", money "amount" and "notes"(must be, can be "")
        [HttpPost("withdraw")]
        public async Task<ActionResult<string>> PostWithdraw([FromBody] dynamic data)
        {
            try
            {
                var acc = await _transactionsService.WithdrawCash(data);
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return StatusCode(215, ex.Message);
            }
        }
        
        // POST api/transactions/send
        // send your money from account to another account
        // need id of "account" from, card "number" to,  money "amount" and "notes"(must be, can be "")
        [HttpPost("send")]
        public async Task<ActionResult<string>> PostSend([FromBody] dynamic data)
        {
            try
            {
                var acc = await _transactionsService.SendMoney(data);
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return StatusCode(215, ex.Message);
            }
        }
    }
}