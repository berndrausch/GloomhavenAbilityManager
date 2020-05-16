using System.Collections.Generic;
using Xunit;

namespace GloomhavenAbilityManager.Logic.Tests
{
    public class IntegrationTestsWithDataSetGeneric : IntegrationTests
    {
        private const string DataDir = @"Data\Generic";

        [Theory]
        [InlineData(2)]
        public void CharacterService_Should_Return_Correct_Number_Of_Characters(int expectedNumber)
        {
            CharacterService_Should_Return_Correct_Number_Of_Characters_Base(DataDir, expectedNumber);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CharacterService_Should_Return_Character(int charId)
        {
            CharacterService_Should_Return_Character_Base(DataDir, charId);
        }

        [Theory]
        [InlineData(1, "Player A", 5, 2, 6, 4)]
        [InlineData(2, "Player B", 6, 3, 6, 6)]
        public void CharacterService_Should_Return_Correct_Character(int charId, string expectedName, int expectedLevel, int expectedClassId, int expectedNumberOfPoolCards, int expectedNumberOfSelectedCards)
        {
            CharacterService_Should_Return_Correct_Character_Base(DataDir, charId, expectedName, expectedLevel, expectedClassId, expectedNumberOfPoolCards, expectedNumberOfSelectedCards);
        }

        [Theory]
        [InlineData(3)]
        public void CharacterClassService_Should_Return_Correct_Number_Of_Classes(int expectedNumber)
        {
            CharacterClassService_Should_Return_Correct_Number_Of_Classes_Base(DataDir, expectedNumber);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CharacterClassService_Should_Return_Class(int classId)
        {
            CharacterClassService_Should_Return_Class_Base(DataDir, classId);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 9)]
        [InlineData(3, 7)]
        public void AbilityCardService_Should_Return_Correct_Number_Of_Cards(int classId, int expectedNumber)
        {
            AbilityCardService_Should_Return_Correct_Number_Of_Cards_Base(DataDir, classId, expectedNumber);
        }

        [Theory]
        [InlineData(1, new int[0], new int[0])]
        [InlineData(2, new int[0], new int[0])]
        [InlineData(1, new[] { 0, 1, 2, 3, 4, 5 }, new[] { 0, 2, 4 })]
        [InlineData(2, new[] { 9, 10, 11, 12, 13, 14, 15 }, new[] { 9, 15 })]
        public void CharacterService_Should_Save_Changed_Cards(int characterId, IEnumerable<int> poolCards, IEnumerable<int> selectedCards)
        {
            CharacterService_Should_Save_Changed_Cards_Base(DataDir, characterId,poolCards, selectedCards);
        }
    }
}