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
    public class CharacterClassRepositoryCsv : ICharacterClassRepository
    {
        public IEnumerable<CharacterClassDataObject> GetAll()
        {
            string fileName = FileNames.Classes;
            
            try
            {
                var csvFileReader = new CsvFileReader<CharacterClassDataObject>(new FileSystem(), fileName);
                return csvFileReader.GetAll();
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to read character classes from {fileName}", ex);
            }
        }
    }
}
