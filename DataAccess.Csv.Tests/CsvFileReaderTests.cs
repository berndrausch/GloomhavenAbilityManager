using System;
using Xunit;
using GloomhavenAbilityManager.DataAccess.Csv;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.Linq;

namespace GloomhavenAbilityManager.DataAccess.Csv.Tests
{
    public class CsvFileReaderTests : IDisposable
    {
        private const string csvFileName = @"dummy.csv";
        private const string csvHeader = "Property1;Property2";
        private CsvFileReader<Dummy> _sut;
        private MockFileSystem _fileSystem;

        public CsvFileReaderTests()
        {
            _fileSystem = new MockFileSystem();
            _sut = new CsvFileReader<Dummy>(_fileSystem, csvFileName);
        }

        public void Dispose()
        {
        }

        [Fact]
        public void GetAll_Should_ReturnEmpyCollection_When_OnlyHeadersPresent()
        {
            AddFileWithContent(_fileSystem, csvFileName, new[] {csvHeader});
            Assert.Empty(_sut.GetAll());
        }

        [Fact]
        public void GetAll_Should_ReturnOneEntry()
        {
            AddFileWithContent(_fileSystem, csvFileName, new[] {csvHeader, "123;test"});
            
            var actualResults = _sut.GetAll();

            Assert.Single(actualResults);

            var actualFirstEntry = actualResults.First();
            
            Assert.Equal(123, actualFirstEntry.Property1);
            Assert.Equal("test", actualFirstEntry.Property2);
            
        }

        private static void AddFileWithContent(MockFileSystem fileSystem, string fileName, IEnumerable<string> fileContent)
        {
            fileSystem.File.WriteAllLines(fileName, fileContent);
        }
    }
}