using System.Collections.Generic;

namespace GloomhavenAbilityManager.Logic.Data
{
    public class Character
    {
        public static readonly Character Default = new Character {Id = -1, Name = "Unknown", ClassId = -1};

        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }

        public IEnumerable<AbilityCard> AvailableCards { get; set; }
        public IEnumerable<AbilityCard> SelectedCards { get; set; }

        public Character()
        {
            AvailableCards = new AbilityCard[0];
            SelectedCards = new AbilityCard[0];        
        }
    }
}