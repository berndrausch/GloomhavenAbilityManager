using System.Collections.Generic;

namespace GloomhavenAbilityManager.DataAccess.Contracts.Data
{
    public class CharacterDataObject
    {
        public static readonly CharacterDataObject Default = new CharacterDataObject {Id = -1, Name = "Unknown", ClassId = -1};

        public int Id { get; set; }
        public string Name { get; set; }

        public int Level {get; set; }
       
        public int ClassId { get; set; }

        public IEnumerable<AbilityCardDataObject> PoolCards { get; set; }
        public IEnumerable<AbilityCardDataObject> SelectedCards { get; set; }

        public CharacterDataObject()
        {
            PoolCards = new AbilityCardDataObject[0];
            SelectedCards = new AbilityCardDataObject[0];        
        }
    }
}