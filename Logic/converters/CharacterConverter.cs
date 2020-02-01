using System;
using System.Collections.Generic;
using System.Text;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.Logic.Contracts.Data;

namespace GloomhavenAbilityManager.Logic.Converters
{
    internal static class CharacterClassConverter
    {
        internal static CharacterClass FromDataObject(CharacterClassDataObject dataObject)
        {
            return new CharacterClass()
            {
                Id = dataObject.Id,
                Name = dataObject.Name,
                NumberOfCards = dataObject.NumberOfCards,
            };
        }

        internal static CharacterClassDataObject ToDataObject(CharacterClass obj)
        {
            return new CharacterClassDataObject()
            {
                Id = obj.Id,
                Name = obj.Name,
                NumberOfCards = obj.NumberOfCards,
            };
        }
    }
}
