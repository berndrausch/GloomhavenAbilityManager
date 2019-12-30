using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloomhavenAbilityManager.Data
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public IEnumerable<int> AvailableCardIds { get; set; }
        public IEnumerable<int> SelectedCardIds { get; set; }
    }
}
