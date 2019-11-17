using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public async Task<User> LoginToAtm(dynamic data)
        {
            try
            {
                string cardNum = data["number"];
                string pin = data["pincode"];


                var card = await _unitOfWork.Repository<Card>().GetAsync(
                    c => c.Number == cardNum && c.PinHash ==
                         CryptoHash.ComputeHash(pin, new MD5CryptoServiceProvider()));
                var ac = await _unitOfWork.Repository<Account>().GetAsync(
                    acc => acc.Id == card.AccountId);
                return await _unitOfWork.Repository<User>().GetAsync(
                    user => user.Passport == ac.OwnerPassport);
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Incorrect \"number\" & \"pincode\"");
            }
            
        }
    }
}