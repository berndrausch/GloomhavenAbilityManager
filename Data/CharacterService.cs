using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Data
{
    public class CharacterService : ICharacterService
    {
        private Character[] _allCharacters = new[]
        {
            new Character {Id = 1, Name = "Bernd", ClassId = 2, AvailableCardIds  = new []{0,1,2,3,4}, SelectedCardIds = new int[0]},
            new Character {Id = 2, Name = "Jenny", ClassId = 3, AvailableCardIds  = new []{5,6,7,8,9,10}, SelectedCardIds = new int[0]},
        };

        public Character GetCharacter(int id)
        {
            return _allCharacters.FirstOrDefault(c => c.Id == id) ?? new Character(){Name = "Unknown"};
        }

        public Task<IEnumerable<Character>> GetCharactersAsync()
        {
            return Task.FromResult<IEnumerable<Character>>(_allCharacters);
        }
    }
}
