using Xunit;
using GloomhavenAbilityManager.DataAccess.Csv;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;

namespace GloomhavenAbilityManager.Logic.Tests
{
    public class IntegrationTests 
    {
        private readonly CharacterClassRepositoryCsv _classRepository;
        private readonly AbilityCardRepositoryCsv _cardRepository;
        private readonly CharacterRepositoryCsv _charRepository;

        protected MockFileSystem FileSystem { get; }

        protected CsvConfiguration CsvConfiguration { get; }

        public IntegrationTests()
        {
            FileSystem = new MockFileSystem();
            CsvConfiguration = new CsvConfiguration();

            _classRepository = new CharacterClassRepositoryCsv(FileSystem, CsvConfiguration);
            _cardRepository = new AbilityCardRepositoryCsv(FileSystem, CsvConfiguration);
            _charRepository = new CharacterRepositoryCsv(FileSystem, CsvConfiguration, _cardRepository);
        }

        protected void CharacterService_Should_Return_Correct_Number_Of_Characters_Base(string dataDir, int expectedNumber)
        {
            MockFileSystemSetup.SetUpFileSystem(FileSystem, CsvConfiguration, dataDir);
            AbilityCardService cardService = new AbilityCardService(_cardRepository);
            CharacterService sut = new CharacterService(_charRepository, cardService);

            var actualCharacters = sut.GetCharacters();

            Assert.Equal(expectedNumber, actualCharacters.Count());
        }

        protected void CharacterService_Should_Return_Character_Base(string dataDir, int charId)
        {
            MockFileSystemSetup.SetUpFileSystem(FileSystem, CsvConfiguration, dataDir);
            AbilityCardService cardService = new AbilityCardService(_cardRepository);
            CharacterService sut = new CharacterService(_charRepository, cardService);

            var actualChar = sut.GetCharacter(charId);

            Assert.Equal(charId, actualChar.Id );
        }

        protected void CharacterService_Should_Return_Correct_Character_Base(string dataDir, int charId, string expectedName, int expectedLevel, int expectedClassId, int expectedNumberOfPoolCards, int expectedNumberOfSelectedCards)
        {
            MockFileSystemSetup.SetUpFileSystem(FileSystem, CsvConfiguration, dataDir);
            AbilityCardService cardService = new AbilityCardService(_cardRepository);
            CharacterService sut = new CharacterService(_charRepository, cardService);

            var actualChar = sut.GetCharacter(charId);

            Assert.Equal(expectedName, actualChar.Name);
            Assert.Equal(expectedLevel, actualChar.Level);
            Assert.Equal(expectedClassId, actualChar.ClassId);
            Assert.Equal(expectedNumberOfPoolCards, actualChar.PoolCards.Count());
            Assert.Equal(expectedNumberOfSelectedCards, actualChar.SelectedCards.Count());
        }

        protected void CharacterClassService_Should_Return_Correct_Number_Of_Classes_Base(string dataDir, int expectedNumber)
        {
            MockFileSystemSetup.SetUpFileSystem(FileSystem, CsvConfiguration, dataDir);
            CharacterClassService sut = new CharacterClassService(_classRepository, _cardRepository);

            var actualClasses = sut.GetClasses();

            Assert.Equal(expectedNumber, actualClasses.Count());
        }

        protected void CharacterClassService_Should_Return_Class_Base(string dataDir, int classId)
        {
            MockFileSystemSetup.SetUpFileSystem(FileSystem, CsvConfiguration, dataDir);
            CharacterClassService sut = new CharacterClassService(_classRepository, _cardRepository);

            var actualClass = sut.GetClass(classId);

            Assert.Equal(classId, actualClass.Id );
        }

        protected void AbilityCardService_Should_Return_Correct_Number_Of_Cards_Base(string dataDir, int classId, int expectedNumber)
        {
            MockFileSystemSetup.SetUpFileSystem(FileSystem, CsvConfiguration, dataDir);
            AbilityCardService sut = new AbilityCardService(_cardRepository);

            var actualResult = sut.GetCharacterClassCards(classId);

            Assert.Equal(expectedNumber, actualResult.Count());
        }

    }
}