using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Contracts.Interfaces;

namespace GloomhavenAbilityManager.DataAccess.Csv
{
    public class CharacterAbilityCardRelation 
    {
        public int CharacterId{ get; set; }
        
        public int AbilityCardId{ get; set; }

        public bool IsSelected{ get; set; }
    }
}