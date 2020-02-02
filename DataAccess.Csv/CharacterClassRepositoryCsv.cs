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
    public class CharacterClassRepositoryCsv : ICharacterClassRepository
    {
        public IEnumerable<CharacterClassDataObject> GetAll()
        {
            try
            {
                using (var reader = new StreamReader(FileNames.Classes))
                {
                    using (var csv = new CsvReader(reader))
                    {
                        var classes = csv.GetRecords<CharacterClassDataObject>().ToList();
                        return classes;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to read character classes from {FileNames.Classes}", ex);
            }
        }
    }
}
