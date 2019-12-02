using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engine.DataAccess.UnitOfWork;
using Engine.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enum;

namespace Engine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly CardsService _cardsService;

        public CardsController(CardsService cards)
        {
            _cardsService = cards;
        }

        // POST api/cards/user
        // get cards 
        // need "passport" user
        [HttpPost("user")]
        public async Task<ActionResult<IEnumerable<Card>>> PostAllUserCards([FromBody] dynamic data)
        {
            try
            {
                var acc = await _cardsService.GetCardsAsync(data);
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return StatusCode(215, ex.Message);
            }
        }
        
        // POST api/cards/change_pin
        // set new pin on card 
        // need "card_num" and "new_pin"
        [HttpPost("change_pin")]
        public async Task<ActionResult> PostChangePin([FromBody] dynamic data)
        {
            try
            {
                await _cardsService.ChangeCardPin(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(215, ex.Message);
            }
        }
        
        // POST api/cards/amount
        // get amount of money 
        // need "number" of card
        [HttpPost("amount")]
        public async Task<ActionResult<decimal>> PostGetMoneyAmount([FromBody] dynamic data)
        {
            try
            {
                var acc = await _cardsService.GetMoneyOnCard(data);
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return StatusCode(215, ex.Message);
            }
        }
        
        // POST api/cards/froze
        // froze account 
        // need "number" card
        [HttpPost("froze")]
        public async Task<ActionResult> PostSetAccFrozen([FromBody] dynamic data)
        {
            try
            {
                await _cardsService.SetAccFrozen(data);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(215, ex.Message);
            }
        }
    }
}