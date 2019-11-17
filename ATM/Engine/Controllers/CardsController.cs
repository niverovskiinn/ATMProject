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
    public class CardsController:ControllerBase
    {
        private readonly CardsService _cardsService;
        public CardsController(CardsService cards)
        {
            _cardsService = cards;
        }
        

        [HttpPost("user")]
        public async Task<ActionResult<IEnumerable<Card>>> PostAllUserCards([FromBody] dynamic userId)
        {
            try
            {
                var acc = await _cardsService.GetCardsAsync(userId);
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}