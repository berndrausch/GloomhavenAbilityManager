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
    public class CharacterRepositoryCsv : ICharacterRepository
    {
       public IEnumerable<Character> GetAll()
        {
            using (var reader = new StreamReader("DataAccess.Csv\\characters.csv"))
            {
                using (var csv = new CsvReader(reader))
                {    
                   return csv.GetRecords<Character>().ToList();
                }
            }
        }
    }
}
