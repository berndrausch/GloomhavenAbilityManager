using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloomhavenAbilityManager.Logic.Contracts;
using GloomhavenAbilityManager.Logic.Data;

namespace GloomhavenAbilityManager.Logic.Services
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
            return _cardRepository.GetAll().First(c => c.Id == id);
        }

        public IEnumerable<AbilityCard> GetCharacterClassCards(int classId)
        {
           return Filter(_cardRepository.GetAll(), classId);
        }

        private IEnumerable<AbilityCard> Filter(IEnumerable<AbilityCard> cardInfos, int classId)
        {
            return cardInfos.Where(c => classId <= 0 || c.ClassId == classId);
        }
    }
}