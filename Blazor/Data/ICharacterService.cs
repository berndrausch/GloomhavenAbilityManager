using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Blazor.Data
{
    public interface ICharacterService
    {
        Character GetCharacter(int id);
        Task<IEnumerable<Character>> GetCharactersAsync();
    }
}