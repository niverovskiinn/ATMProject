using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Engine.DataAccess.UnitOfWork;
using Engine.Services.Utils;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enum;

namespace Engine.Services
{
    public class CardsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CardsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task ChangeCardPin(dynamic data)
        {
            try
            {
                string cardNum = data["card_num"];
                string newPin = data["new_pin"];
                var card = await _unitOfWork.Repository<Card>().GetAsync(c => c.Number == cardNum);
                card.PinHash = CryptoHash.ComputeHash(newPin, new MD5CryptoServiceProvider());
                _unitOfWork.Repository<Card>().Update(card);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Incorrect \"card_num\" or \"new_pin\"");
            }
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(dynamic userPassport)
        {
            try
            {
                string passport = userPassport["passport"];
                var acc = await _unitOfWork.Repository<Account>().GetListAsync(
                    ac => ac.OwnerPassport == passport);

                var cards = new List<Card>();

                foreach (var ac in acc)
                {
                    cards.AddRange(await _unitOfWork.Repository<Card>().GetListAsync(
                        c => c.AccountId == ac.Id));
                }

                return cards;
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Incorrect \"passport\"");
            }
        }


        public async Task<decimal> GetMoneyOnCard(dynamic data)
        {
            try
            {
                string cardNum = data["number"];
                var card = await _unitOfWork.Repository<Card>().GetAsync(c => c.Number == cardNum);
                var acc = await _unitOfWork.Repository<Account>().GetAsync(a => a.Id == card.AccountId);
                return acc.AmountMoney;
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Incorrect card \"number\"");
            }
        }


        public async Task SetAccFrozen(dynamic data)
        {
            try
            {
                string number = data["number"];
                var card = await _unitOfWork.Repository<Card>().GetAsync(c => c.Number == number);
                var account = await _unitOfWork.Repository<Account>().GetAsync(a => a.Id == card.AccountId);
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