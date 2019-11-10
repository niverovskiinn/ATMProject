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

        public async Task<IEnumerable<Card>> GetUserCardsAsync(User user)
        {
            var acc = await _unitOfWork.Repository<Account>().GetListAsync(
                account => account.OwnerPassport == user.Passport);
            IEnumerable<Card> cards = new List<Card>();
            return acc.Aggregate(cards, (current, ac) => current.Concat(ac.Cards.Where(card => card.AccountId == ac.Id)));
        }

        public async Task ChangeCardPin(string cardNum, string newPin)
        {
            var card = await _unitOfWork.Repository<Card>().GetAsync(c => c.Number == cardNum);
            card.PinHash = CryptoHash.ComputeHash(newPin, new MD5CryptoServiceProvider());
            _unitOfWork.Repository<Card>().Update(card);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}