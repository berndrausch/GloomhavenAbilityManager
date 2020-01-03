using System.Collections.Generic;
using System.Threading.Tasks;
using GloomhavenAbilityManager.Logic.Data;

namespace GloomhavenAbilityManager.Logic.Contracts
{
    public interface ICharacterRepository
    {
        IEnumerable<Character> GetAll();

        void SaveAll(IEnumerable<Character> characters);
    }
}