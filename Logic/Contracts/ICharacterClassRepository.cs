using System.Collections.Generic;
using System.Threading.Tasks;
using GloomhavenAbilityManager.Logic.Data;

namespace GloomhavenAbilityManager.Logic.Contracts
{
    public interface ICharacterClassRepository
    {
        IEnumerable<CharacterClass> GetAll();
    }
}