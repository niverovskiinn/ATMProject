using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engine.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enum;

namespace Engine.Services
{
    public class TransactionsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Transaction>> GetUserAllTransAsync(string passport)
        {
            var trans = new List<Transaction>();
            var acc = await _unitOfWork.Repository<Account>()
                .GetListAsync(a => a.OwnerPassport == passport);
            foreach (var ac in acc)
            {
                trans.AddRange(await _unitOfWork.Repository<Transaction>()
                    .GetListAsync(tr => tr.AccountFromId == ac.Id));
            }
            return trans;
        }

        //TODO check date format
        public async Task<IEnumerable<Transaction>> GetUserTransPeriodAsync(Account account, string fromStr, string toStr)
        {
            var from = DateTime.ParseExact(fromStr, "dd.MM.yyyy HH:mm:ss",null);
            var to = DateTime.ParseExact(toStr, "dd.MM.yyyy HH:mm:ss",null);
            var accc = await _unitOfWork.Repository<Account>().GetAsync(acc => acc.Id == account.Id);
           return await _unitOfWork.Repository<Transaction>().GetListAsync(
               tran => tran.DateTimeTr > from && tran.DateTimeTr < to && tran.AccountFromId == accc.Id);
        }


        public async Task<ActionResult<string>> WithdrawCash(Account account, decimal amount)
        {
            amount = (AccountTypeEnum)account.TypeId == AccountTypeEnum.Credit ? amount * 1.12m : amount;
            if (account.AmountMoney < amount)
                return "NOT ENOUGH MONEY";
            _unitOfWork.Repository<Transaction>().Add(new Transaction
            {
                DateTimeTr = DateTime.Now,
                Type = TransactionTypeEnum.Withdraw,
                AmountMoney = amount,
                AccountFromId = account.Id,
                AccountToId = null,
            });
            account.AmountMoney -= amount;
            await _unitOfWork.SaveChangesAsync();
            return "Success";
        }

        public async Task<ActionResult<string>> SendMoney(Account from, string cardNumTo, decimal amount)
        {
            amount = (AccountTypeEnum)from.TypeId == AccountTypeEnum.Credit ? amount * 1.12m : amount;
            if (from.AmountMoney < amount)
                return "NOT ENOUGH MONEY";

            var cardTo = await _unitOfWork.Repository<Card>().GetAsync(c => c.Number == cardNumTo);
            var to = await _unitOfWork.Repository<Account>()
                .GetAsync(account => account.Id == cardTo.AccountId);
            
            _unitOfWork.Repository<Transaction>().Add(new Transaction
            {
                DateTimeTr = DateTime.Now,
                Type = TransactionTypeEnum.ToUser,
                AmountMoney = amount,
                AccountFromId = from.Id,
                AccountToId = to.Id,
            });
            from.AmountMoney -= amount;
            to.AmountMoney += amount;
            await _unitOfWork.SaveChangesAsync();
            return "Success";
        }
        
        
        
    }
}