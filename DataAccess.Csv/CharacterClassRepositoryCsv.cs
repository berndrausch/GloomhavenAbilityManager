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
    public class CharacterClassRepositoryCsv : ICharacterClassRepository
    {
       public IEnumerable<CharacterClassDataObject> GetAll()
        {
            using (var reader = new StreamReader("DataAccess.Csv\\classes.csv"))
            {
                using (var csv = new CsvReader(reader))
                {    
                    var classes = csv.GetRecords<CharacterClassDataObject>().ToList();
                    return classes;
                }
            }
        }
    }
}
