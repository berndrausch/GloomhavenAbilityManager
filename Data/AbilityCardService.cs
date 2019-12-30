using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Data
{
    public class AbilityCardService : IAbilityCardService
    {
        private readonly IAbilityCardRepository _cardRepository;

        public AbilityCardService(IAbilityCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<AbilityCardInfo> GetCardAsync(int id)
        {
            IEnumerable<AbilityCardInfo> allCards = await _cardRepository.GetAllAsync();
            return allCards.First(c => c.Id == id);
        }

        public async Task<IEnumerable<AbilityCardInfo>> GetCardsAsync(int classId)
        {
            IEnumerable<AbilityCardInfo> allCards = await _cardRepository.GetAllAsync();
            return Filter(allCards, classId);
        }

        private IEnumerable<AbilityCardInfo> Filter(IEnumerable<AbilityCardInfo> cardInfos, int classId)
        {
            return cardInfos.Where(c => classId <= 0 || c.ClassId == classId);
        }
    }
}