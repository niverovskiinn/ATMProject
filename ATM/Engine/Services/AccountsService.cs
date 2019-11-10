using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.UnitOfWork;
using Models;

namespace Engine.Services
{
    public class AccountsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}