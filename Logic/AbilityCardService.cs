using System.Collections.Generic;
using System.Linq;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Contracts.Interfaces;
using GloomhavenAbilityManager.Logic.Contracts.Data;
using GloomhavenAbilityManager.Logic.Contracts.Interfaces;
using GloomhavenAbilityManager.Logic.Converters;

namespace GloomhavenAbilityManager.Logic
{
    public class AbilityCardService : IAbilityCardService
    {
        private readonly IAbilityCardRepository _cardRepository;
        
        public AbilityCardService(IAbilityCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public AbilityCard GetCard(int id)
        {
            return AbilityCardConverter.FromDataObject(_cardRepository.GetAll().First(c => c.Id == id));
        }

        public IEnumerable<AbilityCard> GetCharacterClassCards(int classId)
        {
           return Filter(_cardRepository.GetAll(), classId);
        }

        private IEnumerable<AbilityCard> Filter(IEnumerable<AbilityCardDataObject> cardInfos, int classId)
        {
            return cardInfos.Where(c => classId <= 0 || c.ClassId == classId).Select( AbilityCardConverter.FromDataObject);
        }
    }
}