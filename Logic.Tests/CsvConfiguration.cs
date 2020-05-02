using GloomhavenAbilityManager.DataAccess.Contracts.interfaces;
using GloomhavenAbilityManager.DataAccess.Csv;

namespace GloomhavenAbilityManager.Logic.Tests
{
    public class CsvConfiguration : ICsvConfiguration
    {
        public string DataDir { get; set; } = @"c:\data";
        public string CardsFileName { get; set; } = "cards.csv";
        public string CharactersFileName { get; set; } = "characters.csv";
        public string ClassesFileName { get; set; } = "classes.csv";
        public string CharacterCardsFileName { get; set; } = "charcards.csv";
    }
}