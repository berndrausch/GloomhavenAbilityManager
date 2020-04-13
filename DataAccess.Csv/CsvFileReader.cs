using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using CsvHelper;
using GloomhavenAbilityManager.DataAccess.Contracts.Data;

namespace GloomhavenAbilityManager.DataAccess.Csv
{
    public class CsvFileReader<T>
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _fileName;

        public CsvFileReader(IFileSystem fileSystem, string fileName)
        {
            _fileSystem = fileSystem;
            _fileName = fileName;
        }

        public List<T> GetAll()
        {
            using (var reader = new StreamReader(_fileSystem.File.OpenRead(_fileName)))
            {
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.Delimiter = ";";
                    return csv.GetRecords<T>().ToList();
                }
            }
        }
    }
}
