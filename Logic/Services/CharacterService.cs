using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloomhavenAbilityManager.Logic.Contracts;
using GloomhavenAbilityManager.Logic.Data;

namespace GloomhavenAbilityManager.Logic.Services
{
    public class CharacterService : ICharacterService
    {

        private readonly ICharacterRepository _characterRepository;
        private readonly IAbilityCardService _cardService;
        
        public CharacterService(ICharacterRepository characterRepository, IAbilityCardService cardService)
        {
            _characterRepository = characterRepository;
            _cardService = cardService;
        }

        public Character GetCharacter(int id)
        {
            return GetCharacters().FirstOrDefault(c => c.Id == id) ?? Character.Default;
        }

        public IEnumerable<Character> GetCharacters()
        {
            var allcharacters = _characterRepository.GetAll();
            
            return allcharacters;
        }
    }
}