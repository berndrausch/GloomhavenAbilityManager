using System.Collections.Generic;
using System.Threading.Tasks;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;

namespace GloomhavenAbilityManager.DataAccess.Contracts.Interfaces
{
    public interface ICharacterRepository
    {
        IEnumerable<CharacterDataObject> GetAll();

        void SaveAll(IEnumerable<CharacterDataObject> characters);
    }
}