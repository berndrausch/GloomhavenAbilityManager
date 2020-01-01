using System.Collections.Generic;
using System.Threading.Tasks;
using GloomhavenAbilityManager.Logic.Data;

namespace GloomhavenAbilityManager.Logic.Contracts
{
    public interface IAbilityCardService
    {
        AbilityCard GetCard(int id);

        IEnumerable<AbilityCard> GetCharacterClassCards(int characterClassId);
    }
}