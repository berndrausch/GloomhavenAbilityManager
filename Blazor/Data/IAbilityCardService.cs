using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Blazor.Data
{
    public interface IAbilityCardService
    {
        Task<AbilityCardInfo> GetCardAsync(int id);

        Task<IEnumerable<AbilityCardInfo>> GetCardsAsync(int characterClassId);
    }
}