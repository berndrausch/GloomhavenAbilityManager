using System.Collections.Generic;
using System.Threading.Tasks;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;

namespace GloomhavenAbilityManager.DataAccess.Contracts.Interfaces
{
    public interface IAbilityCardRepository
    {
        IEnumerable<AbilityCardDataObject> GetAll();
    }
}