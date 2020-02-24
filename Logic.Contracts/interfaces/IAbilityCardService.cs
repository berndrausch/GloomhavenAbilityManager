using System.Collections.Generic;
using System.Threading.Tasks;
using GloomhavenAbilityManager.Logic.Contracts.Data;

namespace GloomhavenAbilityManager.Logic.Contracts.Interfaces
{
    public interface IAbilityCardService
    {
        AbilityCard GetCard(int id);

        IEnumerable<AbilityCard> GetCharacterClassCards(int characterClassId);

        IEnumerable<AbilityCard> GetAvailableCards(Character character);
    }
}