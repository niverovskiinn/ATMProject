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

    public class TransactionsController : ControllerBase
    {
        private readonly TransactionsService _transactionsService;


        public TransactionsController(TransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }
        
        
    }
}