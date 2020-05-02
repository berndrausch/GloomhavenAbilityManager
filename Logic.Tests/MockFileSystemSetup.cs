using System.IO;
using System.IO.Abstractions.TestingHelpers;
using GloomhavenAbilityManager.DataAccess.Contracts.interfaces;
using GloomhavenAbilityManager.DataAccess.Csv;

namespace GloomhavenAbilityManager.Logic.Tests
{
    public class MockFileSystemSetup
    {
        public static void SetUpFileSystem(MockFileSystem filesystem, ICsvConfiguration configuration, string sourceDirectory)
        {
            filesystem.Directory.CreateDirectory(configuration.DataDir);

            AddClassesFile(filesystem, configuration, sourceDirectory);
            AddCharsFile(filesystem, configuration, sourceDirectory);
            AddCardsFile(filesystem, configuration, sourceDirectory);
            AddCharCardsFile(filesystem, configuration, sourceDirectory);
        }

        private static void AddClassesFile(MockFileSystem filesystem, ICsvConfiguration configuration, string sourceDirectory)
        {
            AddFile(filesystem, configuration, sourceDirectory, configuration.ClassesFileName);
        }
        private static void AddCharsFile(MockFileSystem filesystem, ICsvConfiguration configuration, string sourceDirectory)
        {
            AddFile(filesystem, configuration, sourceDirectory, configuration.CharactersFileName);
        }
        private static void AddCardsFile(MockFileSystem filesystem, ICsvConfiguration configuration, string sourceDirectory)
        {
            AddFile(filesystem, configuration, sourceDirectory, configuration.CardsFileName);
        }
        private static void AddCharCardsFile(MockFileSystem filesystem, ICsvConfiguration configuration, string sourceDirectory)
        {
            AddFile(filesystem, configuration, sourceDirectory, configuration.CharacterCardsFileName);
        }

        private static void AddFile(MockFileSystem filesystem, ICsvConfiguration configuration, string sourceDirectory, string fileName)
        {
            string contentFile = Path.Combine(sourceDirectory, fileName);
            string dstFileName = Path.Combine(configuration.DataDir, fileName);
            AddFileWithContent(filesystem, dstFileName, contentFile);
        }

        private static void AddFileWithContent(MockFileSystem fileSystem, string dstFileName, string contentFile)
        {
            fileSystem.File.WriteAllBytes(dstFileName, File.ReadAllBytes(contentFile));
        }
    }
}