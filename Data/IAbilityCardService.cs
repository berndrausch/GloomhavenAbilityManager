using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Data
{
    public interface IAbilityCardService
    {
        Task<IEnumerable<AbilityCardInfo>> GetCardsAsync();
    }
}