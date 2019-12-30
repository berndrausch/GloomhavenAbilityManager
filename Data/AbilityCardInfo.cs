using System;

namespace GloomhavenAbilityManager.Data
{
    public class AbilityCardInfo
    {
        public int Id {get;set;}

        public int ClassId { get; set; }

        public string Level { get; set; }

        public string Name { get; set; }

        public string ImagePath  { get; set; }

        public bool IsSelected  { get; set; }
    }
}