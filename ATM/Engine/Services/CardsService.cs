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

namespace Engine.Services
{
    public class CardsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CardsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        public async Task ChangeCardPin(string cardNum, string newPin)
        {
            var card = await _unitOfWork.Repository<Card>().GetAsync(c => c.Number == cardNum);
            card.PinHash = CryptoHash.ComputeHash(newPin, new MD5CryptoServiceProvider());
            _unitOfWork.Repository<Card>().Update(card);
            await _unitOfWork.SaveChangesAsync();
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
                cards.AddRange(await  _unitOfWork.Repository<Card>().GetListAsync(
                    c => c.AccountId == ac.Id));
            }
            return cards;
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Incorrect \"passport\"");
            }
        }
        
    }
}