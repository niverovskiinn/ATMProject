using System.Collections.Generic;
using System.Threading.Tasks;
using Engine.DataAccess.UnitOfWork;
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

//        public async Task<IEnumerable<Account>> GetUserAccountsAsync(User user)
//        {
//            return await _unitOfWork.Repository<Account>().GetListAsync(
//                acc => acc == user.Passport);
//        }

        public async Task AddTransType()
        {
            
        }
        
    }
}