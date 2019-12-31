using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Blazor.Data
{
    public interface IAbilityCardRepository
    {
        Task<IEnumerable<AbilityCardInfo>> GetAllAsync();
    }
}