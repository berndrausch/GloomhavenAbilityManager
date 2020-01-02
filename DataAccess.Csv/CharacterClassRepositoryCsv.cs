using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using GloomhavenAbilityManager.Logic.Contracts;
using GloomhavenAbilityManager.Logic.Data;

namespace GloomhavenAbilityManager.DataAccess.Csv
{
    public class CharacterClassRepositoryCsv : ICharacterClassRepository
    {
       public IEnumerable<CharacterClass> GetAll()
        {
            using (var reader = new StreamReader("DataAccess.Csv\\classes.csv"))
            {
                using (var csv = new CsvReader(reader))
                {    
                    var classes = csv.GetRecords<CharacterClass>().ToList();
                    return classes;
                }
            }
        }
    }
}
