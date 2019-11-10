using Engine.DataAccess.UnitOfWork;

namespace Engine.Services
{
    public class CardsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CardsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}