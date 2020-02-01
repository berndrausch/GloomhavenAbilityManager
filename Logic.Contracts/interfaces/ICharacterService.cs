using System.Collections.Generic;
using System.Threading.Tasks;
using GloomhavenAbilityManager.Logic.Contracts.Data;

namespace GloomhavenAbilityManager.Logic.Contracts.Interfaces
{
    public interface ICharacterService
    {
        Character GetCharacter(int id);
        void UpdateCharacter(Character c);
        IEnumerable<Character> GetCharacters();
    }
}