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

        public async Task<IEnumerable<Transaction>> GetUserAllTransAsync(dynamic data)
        {
            try
            {
                string passport = data["passport"];
                var trans = new List<Transaction>();
                var acc = await _unitOfWork.Repository<Account>()
                    .GetListAsync(a => a.OwnerPassport == passport);
                foreach (var ac in acc)
                {
                    trans.AddRange(await _unitOfWork.Repository<Transaction>()
                        .GetListAsync(tr => tr.AccountFromId == ac.Id ||
                                            tr.AccountToId == ac.Id && tr.AccountToId != tr.AccountFromId));
                }
                return trans;
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Incorrect \"passport\"");
            }
        }

        public async Task<IEnumerable<Transaction>> GetUserTransPeriodAsync(dynamic data)
        {
            try
            {
                int id = data["account"];
                var account = await _unitOfWork.Repository<Account>().GetAsync(ac => ac.Id == id);
                DateTime from = DateTime.ParseExact(data["from"], "dd.MM.yyyy HH:mm:ss", null);
                DateTime to = DateTime.ParseExact(data["to"], "dd.MM.yyyy HH:mm:ss", null);
                var accc = await _unitOfWork.Repository<Account>().GetAsync(acc => acc.Id == account.Id);
                return await _unitOfWork.Repository<Transaction>().GetListAsync(
                    tran => tran.DateTime > from && tran.DateTime < to && (tran.AccountFromId == accc.Id ||
                                                                               tran.AccountToId == accc.Id &&
                                                                               tran.AccountToId != tran.AccountFromId));
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Incorrect \"from\" or \"to\" or \"account\" ");
            }
        }


        public async Task<ActionResult<string>> WithdrawCash(dynamic data)
        {
            try
            {
                decimal amount = data["amount"];
                int id = data["account"];
                string notes = data["notes"];
                var account = await _unitOfWork.Repository<Account>().GetAsync(ac => ac.Id == id);
                amount = (AccountTypeEnum) account.TypeId == AccountTypeEnum.Credit ? amount * 1.15m : amount;
                if (account.AmountMoney < amount)
                    return "NOT ENOUGH MONEY";
                _unitOfWork.Repository<Transaction>().Add(new Transaction
                {
                    DateTime = DateTime.Now,
                    TypeId = (int) TransactionTypeEnum.Withdraw,
                    AmountMoney = amount,
                    AccountFromId = account.Id,
                    AccountToId = null,
                    Notes = (AccountTypeEnum) account.TypeId == AccountTypeEnum.Credit
                        ? "15 percent fee\n" + notes
                        : notes
                });
                account.AmountMoney -= amount;
                await _unitOfWork.SaveChangesAsync();
                return "Success";
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Incorrect \"amount\" or \"account\" or \"notes\"");
            }
        }

        public async Task<ActionResult<string>> SendMoney(dynamic data)
        {
            try
            {
                string cardNumTo = data["number"];
                decimal amount = data["amount"];
                int id = data["account"];
                string notes = data["notes"];
                var from = await _unitOfWork.Repository<Account>().GetAsync(ac => ac.Id == id);
                amount = (AccountTypeEnum) from.TypeId == AccountTypeEnum.Credit ? amount * 1.1m : amount;
                if (from.AmountMoney < amount)
                    return "NOT ENOUGH MONEY";

                var cardTo = await _unitOfWork.Repository<Card>().GetAsync(c => c.Number == cardNumTo);
                var to = await _unitOfWork.Repository<Account>()
                    .GetAsync(account => account.Id == cardTo.AccountId);

                _unitOfWork.Repository<Transaction>().Add(new Transaction
                {
                    DateTime = DateTime.Now,
                    TypeId = (int) TransactionTypeEnum.ToUser,
                    AmountMoney = amount,
                    AccountFromId = from.Id,
                    AccountToId = to.Id,
                    Notes = (AccountTypeEnum) from.TypeId == AccountTypeEnum.Credit
                        ? "10 percent fee\n" + notes
                        : notes});
                from.AmountMoney -= amount;
                to.AmountMoney += amount;
                await _unitOfWork.SaveChangesAsync();
                return "Success";
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Incorrect \"number\" or \"amount\" or \"account\" or \"notes\"");
            }
        }

        public async Task<ActionResult<string>> DepositMoney(dynamic data)
        {
            try
            {
                decimal amount = data["amount"];
                int id = data["account"];
                var acc = await _unitOfWork.Repository<Account>().GetAsync(ac => ac.Id == id);
                _unitOfWork.Repository<Transaction>().Add(new Transaction
                {
                    DateTime = DateTime.Now,
                    TypeId = (int) TransactionTypeEnum.Deposit,
                    AmountMoney = amount,
                    AccountFromId = acc.Id,
                    AccountToId = acc.Id,
                });
                acc.AmountMoney += amount;
                await _unitOfWork.SaveChangesAsync();
                return "Success";
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Incorrect \"amount\" or \"account\" ");
            }
        }
        
    }
}