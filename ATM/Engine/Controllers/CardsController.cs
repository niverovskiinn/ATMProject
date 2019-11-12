using System;
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
        private readonly IUnitOfWork _unitOfWork;
        public CardsController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        
//        [HttpGet]
//        public ActionResult<IEnumerable<string>> Get()
//        {
//        }
//        
//        [HttpGet("{id}")]
//        public ActionResult<IEnumerable<string>> Get(int id)
//        {
//            return new[] {"value1", "value2"};
//        }
    }
}