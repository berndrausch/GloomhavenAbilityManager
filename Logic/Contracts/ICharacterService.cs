using System.Collections.Generic;
using System.Threading.Tasks;
using GloomhavenAbilityManager.Logic.Data;

namespace GloomhavenAbilityManager.Logic.Contracts
{
    public interface ICharacterService
    {
        Character GetCharacter(int id);
        void UpdateCharacter(Character c);
        IEnumerable<Character> GetCharacters();
    }
}