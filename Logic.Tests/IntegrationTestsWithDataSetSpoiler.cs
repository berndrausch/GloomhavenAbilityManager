using Xunit;

namespace GloomhavenAbilityManager.Logic.Tests
{
    public class IntegrationTestsWithDataSetSpoiler : IntegrationTests
    {
        private const string DataDir = @"Data\Spoiler";

        [Theory]
        [InlineData(4)]
        public void CharacterService_Should_Return_Correct_Number_Of_Characters(int expectedNumber)
        {
            CharacterService_Should_Return_Correct_Number_Of_Characters_Base(DataDir, expectedNumber);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CharacterService_Should_Return_Character(int charId)
        {
            CharacterService_Should_Return_Character_Base(DataDir, charId);
        }

        [Theory]
        [InlineData(1, "B", 7, 2, 19, 11)]
        [InlineData(2, "J", 4, 6, 19, 0)]
        [InlineData(3, "J_2", 5, 7, 17, 10)]
        [InlineData(4, "B_2", 9, 8, 20, 9)]
        public void CharacterService_Should_Return_Correct_Character(int charId, string expectedName, int expectedLevel, int expectedClassId, int expectedNumberOfPoolCards, int expectedNumberOfSelectedCards)
        {
            CharacterService_Should_Return_Correct_Character_Base(DataDir, charId, expectedName, expectedLevel, expectedClassId, expectedNumberOfPoolCards, expectedNumberOfSelectedCards);
        }

        [Theory]
        [InlineData(12)]
        public void CharacterClassService_Should_Return_Correct_Number_Of_Classes(int expectedNumber)
        {
            CharacterClassService_Should_Return_Correct_Number_Of_Classes_Base(DataDir, expectedNumber);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        public void CharacterClassService_Should_Return_Class(int classId)
        {
            CharacterClassService_Should_Return_Class_Base(DataDir, classId);
        }

        [Theory]
        [InlineData(1, 29)]
        [InlineData(2, 30)]
        [InlineData(3, 29)]
        [InlineData(4, 28)]
        [InlineData(5, 27)]
        [InlineData(6, 31)]
        [InlineData(7, 29)]
        [InlineData(8, 28)]
        [InlineData(9, 28)]
        [InlineData(10, 29)]
        [InlineData(11, 30)]
        [InlineData(12, 31)]
        public void AbilityCardService_Should_Return_Correct_Number_Of_Cards(int classId, int expectedNumber)
        {
            AbilityCardService_Should_Return_Correct_Number_Of_Cards_Base(DataDir, classId, expectedNumber);
        }

        [Fact]
        public void AddCharacter()
        {
            CharacterService_Should_Add_Character_Base(DataDir, 12, "B_3");
        }
    }
}