using System;
using Xunit;
using GloomhavenAbilityManager.DataAccess.Csv;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.Linq;

namespace GloomhavenAbilityManager.DataAccess.Csv.Tests
{
    public class CsvFileWriterTests : IDisposable
    {
        private const string csvFileName = @"dummy.csv";
        private const string csvHeader = "Property1;Property2";
        private CsvFileWriter<Dummy> _sut;
        private MockFileSystem _fileSystem;

        private List<Dummy> _dummies;

        public CsvFileWriterTests()
        {
            _fileSystem = new MockFileSystem();
            _sut = new CsvFileWriter<Dummy>(_fileSystem, csvFileName);
            _dummies = new List<Dummy>
            {
                new Dummy{Property1=1, Property2="11"},
                new Dummy{Property1=2, Property2="22"} 
            };
        }

        public void Dispose()
        {
        }

        [Fact]
        public void SaveAll_Should_CreateFile_When_NoFileExists()
        {
            _sut.SaveAll(_dummies);

            Assert.True(_fileSystem.File.Exists(csvFileName));
            Assert.Equal(_dummies.Count + 1, _fileSystem.File.ReadAllLines(csvFileName).Count());
        }

        [Fact]
        public void SaveAll_Should_OverrideFile_When_SmallFileExists()
        {
            AddFileWithContent(_fileSystem, csvFileName, new[] {csvHeader, "123;test"});

            _sut.SaveAll(_dummies);

            Assert.Equal(_dummies.Count + 1, _fileSystem.File.ReadAllLines(csvFileName).Count());
        }

        [Fact]
        public void SaveAll_Should_OverrideFile_When_LargeFileExists()
        {
            var content = new string [20];
            for(int i = 0; i< content.Length; i++)
            {
                content[i] = string.Concat(Enumerable.Repeat($"ab{i}", 10 + i));
            }

            AddFileWithContent(_fileSystem, csvFileName, content);

            _sut.SaveAll(_dummies);

            Assert.Equal(_dummies.Count + 1, _fileSystem.File.ReadAllLines(csvFileName).Count());
        }

        private static void AddFileWithContent(MockFileSystem fileSystem, string fileName, IEnumerable<string> fileContent)
        {
            fileSystem.File.WriteAllLines(fileName, fileContent);
        }
    }
}