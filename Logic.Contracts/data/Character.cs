using System.Collections.Generic;

namespace GloomhavenAbilityManager.Logic.Contracts.Data
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Level {get; set; }
        
        public int ClassId { get; set; }

        public IEnumerable<AbilityCard> PoolCards { get; set; }

        public IEnumerable<AbilityCard> SelectedCards { get; set; }

        public Character()
        {
            PoolCards = new AbilityCard[0];
            SelectedCards = new AbilityCard[0];        
        }
    }
}