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
    public class CsvFileWriter<T>
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _fileName;

        public CsvFileWriter(IFileSystem fileSystem, string fileName)
        {
            _fileSystem = fileSystem;
            _fileName = fileName;
        }

        public void SaveAll(IEnumerable<T> records)
        {
            if (_fileSystem.File.Exists(_fileName))
            {
                _fileSystem.File.Delete(_fileName);
            }

            using (var writer = new StreamWriter(_fileSystem.File.OpenWrite(_fileName)))
            {
                using (var csv = new CsvWriter(writer))
                {
                    csv.Configuration.Delimiter = ";";
                     csv.WriteRecords(records);
                }
            }
        }
    }
}
