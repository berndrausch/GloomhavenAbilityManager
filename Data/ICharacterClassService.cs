using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Data
{
    public interface ICharacterClassService
{
    public Task<IEnumerable<CharacterClass>> GetClassesAsync();
}
}