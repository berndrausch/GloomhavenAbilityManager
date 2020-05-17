using Xunit;
using GloomhavenAbilityManager.DataAccess.Csv;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Collections.Generic;
using GloomhavenAbilityManager.Logic.Contracts.Data;

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

        protected void CharacterService_Should_Save_Changed_Cards_Base(string dataDir, int characterId, IEnumerable<int> poolCards, IEnumerable<int> selectedCards)
        {
            MockFileSystemSetup.SetUpFileSystem(FileSystem, CsvConfiguration, dataDir);

            AbilityCardService cardService = new AbilityCardService(_cardRepository);
            CharacterService sut = new CharacterService(_charRepository, cardService);

            Character character = sut.GetCharacter(characterId);

            character.PoolCards = poolCards.Select( id => cardService.GetCard(id)).ToList();
            character.SelectedCards = selectedCards.Select( id => cardService.GetCard(id)).ToList();

            sut.UpdateCharacter(character);

            Character actualCharacter = sut.GetCharacter(characterId);

            Assert.Equal(poolCards.Count(), actualCharacter.PoolCards.Count());
            Assert.Equal(selectedCards.Count(), actualCharacter.SelectedCards.Count());

            foreach(var id in poolCards)
            {
                Assert.Contains(id, actualCharacter.PoolCards.Select( card => card.Id));
            }

            foreach(var id in selectedCards)
            {
                Assert.Contains(id, actualCharacter.SelectedCards.Select( card => card.Id));
            }
        }

        protected void CharacterService_Should_Save_New_Character_Base(string dataDir, string name, int classId, IEnumerable<int> poolCards, IEnumerable<int> selectedCards)
        {
            MockFileSystemSetup.SetUpFileSystem(FileSystem, CsvConfiguration, dataDir);

            AbilityCardService cardService = new AbilityCardService(_cardRepository);
            CharacterService sut = new CharacterService(_charRepository, cardService);

            Character character = new Character()
            {
                Id = -1,
                Name = name,
                ClassId = classId,
                PoolCards = poolCards.Select( cid => cardService.GetCard(cid)).ToList(),
                SelectedCards = selectedCards.Select( cid => cardService.GetCard(cid)).ToList()
            };
            
            sut.AddCharacter(character);

            Assert.True(character.Id >= 0, $"Character Id should be >= 0, but is {character.Id}");
            Assert.True(sut.GetCharacters().Any( c => c.Id == character.Id), "GetCharacters should contain new character, but only contains " + string.Join(",", sut.GetCharacters().Select( c => c.Id.ToString())));

            Character actualCharacter = sut.GetCharacter(character.Id);

            Assert.Equal(character.Id, actualCharacter.Id);
            Assert.Equal(name, actualCharacter.Name);
            Assert.Equal(classId, actualCharacter.ClassId);
            Assert.Equal(poolCards.Count(), actualCharacter.PoolCards.Count());
            Assert.Equal(selectedCards.Count(), actualCharacter.SelectedCards.Count());
        }
    }
}