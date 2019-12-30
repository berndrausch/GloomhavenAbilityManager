using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Data
{
    public class CharacterClassService : ICharacterClassService
    {
        private readonly IAbilityCardRepository _cardRepository;

        private readonly CharacterClass[] _allClasses =
        {
            new CharacterClass {Id = 0, Name = "Alle", NumberOfCards = 0},
            new CharacterClass {Id = 1, Name = "Barbar", NumberOfCards = 10},
            new CharacterClass {Id = 2, Name = "Felsenherz", NumberOfCards = 11},
            new CharacterClass {Id = 3, Name = "Tüftler", NumberOfCards = 12},
        };

        public CharacterClassService(IAbilityCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public CharacterClass GetClass(int id)
        {
            return _allClasses.FirstOrDefault(c => c.Id == id) ?? _allClasses[0];
        }

        public async Task<IEnumerable<CharacterClass>> GetClassesAsync()
        {
            IEnumerable<AbilityCardInfo> allCards = await _cardRepository.GetAllAsync();
            return allCards.Select(c => c.ClassId).Distinct().Select(GetClass);
        }
    }
}