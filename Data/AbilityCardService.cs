using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Data
{

    public class AbilityCardService : IAbilityCardService
    {
        private IAbilityCardRepository _cardRepository;

        public AbilityCardService(IAbilityCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<AbilityCardInfo> GetCardAsync(int id)
        {
            IEnumerable<AbilityCardInfo> allCards = await _cardRepository.GetAllAsync();
            return allCards.First( c => c.Id == id);
        }

        public async Task<IEnumerable<AbilityCardInfo>> GetCardsAsync(CharacterClass characterClass)
        {
            IEnumerable<AbilityCardInfo> allCards = await _cardRepository.GetAllAsync();
            return Filter(allCards, characterClass);       
        }

        private IEnumerable<AbilityCardInfo> Filter(IEnumerable<AbilityCardInfo> cardInfos, CharacterClass characterClass)
        {
            if (characterClass == CharacterClass.All)
            {
                return cardInfos;
            }

            return cardInfos.Where( c => c.Class == characterClass);   
        }
    }
}