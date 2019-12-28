using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<AbilityCardInfo>> GetCardsAsync()
        {
            return await _cardRepository.GetAllAsync();          
        }
    }
}