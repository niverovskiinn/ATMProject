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
            IEnumerable<Transaction> trans = new List<Transaction>();
           
            foreach (var ac in await _unitOfWork.Repository<Account>()
                .GetListAsync(a => a.OwnerPassport == passport))
            {
                trans = trans.Concat(await _unitOfWork.Repository<Transaction>()
                    .GetListAsync(tr => tr.AccountFromId == ac.Id));
            }
            
            return trans;
        }

        //TODO check date format
        public async Task<IEnumerable<Transaction>> GetUserTransPeriodAsync(Account account, string fromStr, string toStr)
        {
            var from = DateTime.ParseExact(fromStr, "yyyy-MM-dd HH:mm tt",null);
            var to = DateTime.ParseExact(toStr, "yyyy-MM-dd HH:mm tt",null);
            var accc = await _unitOfWork.Repository<Account>().GetAsync(acc => acc.Id == account.Id);
           return await _unitOfWork.Repository<Transaction>().GetListAsync(
               tran => tran.DateTime > from && tran.DateTime < to && tran.AccountFromId == accc.Id);
        }


        public async Task<ActionResult<string>> WithdrawCash(Account account, decimal amount)
        {
            amount = account.Type == AccountTypeEnum.Credit ? amount * 1.12m : amount;
            if (account.AmountMoney < amount)
                return "NOT ENOUGH MONEY";
            _unitOfWork.Repository<Transaction>().Add(new Transaction
            {
                DateTime = DateTime.Now,
                Type = TransactionTypeEnum.Withdraw,
                AmountMoney = amount,
                AccountFromId = account.Id,
                AccountToId = null,
                From = account,
                To = null
            });
            account.AmountMoney -= amount;
            await _unitOfWork.SaveChangesAsync();
            return "Success";
        }

        public async Task<ActionResult<string>> SendMoney(Account from, string cardNumTo, decimal amount)
        {
            amount = from.Type == AccountTypeEnum.Credit ? amount * 1.12m : amount;
            if (from.AmountMoney < amount)
                return "NOT ENOUGH MONEY";
            
            var to = await _unitOfWork.Repository<Account>()
                .GetAsync(account => account.Cards.Any(card => card.Number == cardNumTo));
            
            _unitOfWork.Repository<Transaction>().Add(new Transaction
            {
                DateTime = DateTime.Now,
                Type = TransactionTypeEnum.ToUser,
                AmountMoney = amount,
                AccountFromId = from.Id,
                AccountToId = to.Id,
                From = from,
                To = to
            });
            from.AmountMoney -= amount;
            to.AmountMoney += amount;
            await _unitOfWork.SaveChangesAsync();
            return "Success";
        }
        
        
        
    }
}