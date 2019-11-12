using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Engine.DataAccess.UnitOfWork;
using Engine.Services.Utils;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Engine.Services
{
    public class UsersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

//        public async Task<User> LoginToAtm(dynamic data)
//        {
//            string cardNum = data["number"];
//            string pin = data["pincode"];
//            var ac = await  _unitOfWork.Repository<Account>().GetAsync(
//                acc => acc.Cards.Any(card => card.Number == cardNum));
//            return await _unitOfWork.Repository<User>().GetAsync(
//                user => user.Passport == ac.OwnerPassport
//                        && ac.Cards.SingleOrDefault(card => card.Number == cardNum).PinHash
//                        == CryptoHash.ComputeHash(pin, new MD5CryptoServiceProvider()));
//        }
    }
}