using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.Logic.Contracts.Data;

namespace GloomhavenAbilityManager.Logic.Converters
{
    internal static class CharacterConverter
    {
        internal static Character FromDataObject(CharacterDataObject dataObject)
        {
            return new Character()
            {
                Id = dataObject.Id,
                Name = dataObject.Name,
                Level = dataObject.Level,
                ClassId = dataObject.ClassId,
                AvailableCards = dataObject.AvailableCards.Select( AbilityCardConverter.FromDataObject),
                SelectedCards = dataObject.SelectedCards.Select(AbilityCardConverter.FromDataObject),
            };
        }

        internal static CharacterDataObject ToDataObject(Character obj)
        {
            return new CharacterDataObject()
            {
                Id = obj.Id,
                Name = obj.Name,
                Level = obj.Level,
                ClassId = obj.ClassId,
                AvailableCards = obj.AvailableCards.Select(AbilityCardConverter.ToDataObject),
                SelectedCards = obj.SelectedCards.Select(AbilityCardConverter.ToDataObject),
            };
        }
    }
}
