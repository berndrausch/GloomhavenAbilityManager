using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Contracts.Exceptions;
using GloomhavenAbilityManager.DataAccess.Contracts.Interfaces;

namespace GloomhavenAbilityManager.DataAccess.Csv
{
    public class AbilityCardRepositoryCsv : IAbilityCardRepository
    {
        public IEnumerable<AbilityCardDataObject> GetAll()
        {
            try
            {
                using (var reader = new StreamReader(FileNames.Cards))
                {
                    using (var csv = new CsvReader(reader))
                    {
                        var records = csv.GetRecords<AbilityCardDataObject>().ToList();
                        return records;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to read cards from {FileNames.Cards}", ex);
            }
        }
    }
}
