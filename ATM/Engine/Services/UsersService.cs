using System.Collections.Generic;
using System.Threading.Tasks;
using Engine.DataAccess.UnitOfWork;
using Models;

namespace Engine.Services
{
    public class UsersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _unitOfWork.Repository<User>().GetAllAsync();
        }

        public async Task<User> GetUser(string passport)
        {
            return await _unitOfWork.Repository<User>().GetAsync(user => user.Passport == passport);
        }
        
        
    }
}