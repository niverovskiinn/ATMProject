using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engine.DataAccess.UnitOfWork;
using Models;
using Models.Enum;

namespace Engine.Services
{
    public class AccountsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<Account>> GetUserAccountsAsync(dynamic data)
        {
            try
            {
                string passport = data["passport"];
                return await _unitOfWork.Repository<Account>().GetListAsync(
                    acc => acc.OwnerPassport == passport);
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Incorrect \"passport\"");
            }
        }

        public async Task<Account> GetAccAsync(dynamic data)
        {
            try
            {
                int id = data["id"];
                return await _unitOfWork.Repository<Account>().GetAsync(
                    acc => acc.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Incorrect \"id\"");
            }
        }

        public async Task SetAccFrozen(dynamic data)
        {
            try
            {
                int id = data["id"];
                var account = await _unitOfWork.Repository<Account>().GetAsync(a => a.Id == id);
                account.StatusId = (int) AccountStatusEnum.Frozen;
                _unitOfWork.Repository<Account>().Update(account);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Incorrect \"id\"");
            }
        }
    }
}