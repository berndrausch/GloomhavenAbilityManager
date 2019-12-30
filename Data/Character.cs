﻿using System.Collections.Generic;

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