using System.Collections.Generic;
using System.Threading.Tasks;
using GloomhavenAbilityManager.Logic.Data;

namespace GloomhavenAbilityManager.Logic.Contracts
{
    public interface IAbilityCardService
    {
        Task<AbilityCardInfo> GetCardAsync(int id);

        Task<IEnumerable<AbilityCardInfo>> GetCardsAsync(int characterClassId);
    }
}