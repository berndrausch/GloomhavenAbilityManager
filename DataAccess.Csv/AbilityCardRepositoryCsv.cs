using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Contracts.Exceptions;
using GloomhavenAbilityManager.DataAccess.Contracts.interfaces;
using GloomhavenAbilityManager.DataAccess.Contracts.Interfaces;

namespace GloomhavenAbilityManager.DataAccess.Csv
{
    public class AbilityCardRepositoryCsv : IAbilityCardRepository
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICsvConfiguration _configuration;

        public AbilityCardRepositoryCsv(IFileSystem fileSystem, ICsvConfiguration configuration)
        {
            _fileSystem = fileSystem;
            _configuration = configuration;
        }

        public IEnumerable<AbilityCardDataObject> GetAll()
        {
            string fileName = Path.Combine(_configuration.DataDir, _configuration.CardsFileName);

            try
            {
                var csvFileReader = new CsvFileReader<AbilityCardDataObject>(_fileSystem, fileName);
                return csvFileReader.GetAll();
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Unable to read cards from {fileName}", ex);
            }
        }
    }
}
