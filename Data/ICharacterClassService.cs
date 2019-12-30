using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Data
{
    public interface ICharacterClassService
    {
        CharacterClass GetClass(int id);
        Task<IEnumerable<CharacterClass>> GetClassesAsync();
    }
}