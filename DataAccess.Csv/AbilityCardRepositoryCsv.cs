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
    public class AbilityCardRepositoryCsv : IAbilityCardRepository
    {
       public IEnumerable<AbilityCard> GetAll()
        {
            using (var reader = new StreamReader("DataAccess.Csv\\cards.csv"))
            {
                using (var csv = new CsvReader(reader))
                {    
                    var records = csv.GetRecords<AbilityCard>().ToList();
                    return records;
                }
            }
        }
    }
}
