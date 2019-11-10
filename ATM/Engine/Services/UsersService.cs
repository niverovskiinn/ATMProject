using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Engine.DataAccess.UnitOfWork;
using Engine.Services.Utils;
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

        public async Task<User> GetUser(string cardNum, string pin)
        {
            var ac = await _unitOfWork.Repository<Account>().GetAsync(
                acc => acc.Cards.Any(card => card.Number == cardNum));
            return await _unitOfWork.Repository<User>().GetAsync(
                user => user.Passport == ac.OwnerPassport 
                        && ac.Cards.SingleOrDefault(card => card.Number == cardNum).PinHash 
                        == CryptoHash.ComputeHash(pin, new MD5CryptoServiceProvider()));
        }

        public async Task AddUser(User us)
        {
            _unitOfWork.Repository<User>().Add(us);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdUser(User us)
        {
            _unitOfWork.Repository<User>().Update(us);
            await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task DeleteUser(User us)
        {
            _unitOfWork.Repository<User>().Delete(us);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}