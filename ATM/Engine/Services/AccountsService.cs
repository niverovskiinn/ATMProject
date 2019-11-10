using Engine.DataAccess.UnitOfWork;

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