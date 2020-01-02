using System.Collections.Generic;

namespace GloomhavenAbilityManager.Logic.Data
{
    public class Character
    {
        public static readonly Character Default = new Character {Id = -1, Name = "Unknown", ClassId = -1};

        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public IEnumerable<int> AvailableCardIds { get; set; }
        public IEnumerable<int> SelectedCardIds { get; set; }

        public Character()
        {
            AvailableCardIds = new int[0];
            SelectedCardIds = new int[0];        
        }
    }
}