using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Data
{

    public class CharacterClassService : ICharacterClassService
    {
        private IAbilityCardRepository _cardRepository;

        public CharacterClassService(IAbilityCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<IEnumerable<CharacterClass>> GetClassesAsync()
        {
            IEnumerable<AbilityCardInfo> allCards = await _cardRepository.GetAllAsync();
            return allCards.Select( c => c.Class).Distinct();      
        }
    }
}