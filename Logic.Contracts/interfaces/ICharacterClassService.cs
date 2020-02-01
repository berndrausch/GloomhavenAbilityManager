using System.Collections.Generic;
using System.Threading.Tasks;
using GloomhavenAbilityManager.Logic.Contracts.Data;

namespace GloomhavenAbilityManager.Logic.Contracts.Interfaces
{
    public interface ICharacterClassService
    {
        CharacterClass GetClass(int id);
        IEnumerable<CharacterClass> GetClasses();
    }
}