using System;
using System.Collections.Generic;
using System.Text;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.Logic.Contracts.Data;

namespace GloomhavenAbilityManager.Logic.Converters
{
    internal static class AbilityCardConverter
    {
        internal static AbilityCard FromDataObject(AbilityCardDataObject dataObject)
        {
            return new AbilityCard()
            {
                Id = dataObject.Id,
                ClassId = dataObject.ClassId,
                Level = dataObject.Level,
                Name = dataObject.Name,
                ImagePath = dataObject.ImagePath,
            };
        }

        internal static AbilityCardDataObject ToDataObject(AbilityCard obj)
        {
            return new AbilityCardDataObject()
            {
                Id = obj.Id,
                ClassId = obj.ClassId,
                Level = obj.Level,
                Name = obj.Name,
                ImagePath = obj.ImagePath,
            };
        }
    }
}
