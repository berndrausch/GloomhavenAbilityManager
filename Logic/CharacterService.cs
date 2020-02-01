using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Contracts.Interfaces;
using GloomhavenAbilityManager.Logic.Contracts.Data;
using GloomhavenAbilityManager.Logic.Contracts.Interfaces;
using GloomhavenAbilityManager.Logic.Converters;

namespace GloomhavenAbilityManager.Logic
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

        public void UpdateCharacter(Character character)
        {
            var allCharacters = _characterRepository.GetAll().ToList();
            allCharacters.RemoveAll(c => c.Id == character.Id);
            allCharacters.Add(CharacterConverter.ToDataObject(character));
            _characterRepository.SaveAll(allCharacters);
        }

        public IEnumerable<Character> GetCharacters()
        {
            return _characterRepository.GetAll().Select(CharacterConverter.FromDataObject);
        }
    }
}