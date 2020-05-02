using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using CsvHelper;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Contracts.Exceptions;
using GloomhavenAbilityManager.DataAccess.Contracts.interfaces;
using GloomhavenAbilityManager.DataAccess.Contracts.Interfaces;

namespace GloomhavenAbilityManager.DataAccess.Csv
{
    public class CharacterClassRepositoryCsv : ICharacterClassRepository
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICsvConfiguration _configuration;

        public CharacterClassRepositoryCsv(IFileSystem fileSystem, ICsvConfiguration configuration)
        {
            _fileSystem = fileSystem;
            _configuration = configuration;
        }

        public IEnumerable<CharacterClassDataObject> GetAll()
        {
            string fileName = Path.Combine(_configuration.DataDir, _configuration.ClassesFileName);
            
            try
            {
                var csvFileReader = new CsvFileReader<CharacterClassDataObject>(_fileSystem, fileName);
                return csvFileReader.GetAll();
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to read character classes from {fileName}", ex);
            }
        }
    }
}
