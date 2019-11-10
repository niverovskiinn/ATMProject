using Engine.DataAccess.UnitOfWork;

namespace Engine.Services
{
    public class TransactionsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}