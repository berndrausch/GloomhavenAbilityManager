using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
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
            string fileName = FileNames.Cards;

            try
            {
                var csvFileReader = new CsvFileReader<AbilityCardDataObject>(new FileSystem(), fileName);
                return csvFileReader.GetAll();
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to read cards from {fileName}", ex);
            }
        }
    }
}
