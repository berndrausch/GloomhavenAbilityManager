using GloomhavenAbilityManager.DataAccess.Contracts.interfaces;

namespace GloomhavenAbilityManager.DataAccess.Csv
{
    public class CsvConfiguration : ICsvConfiguration
    {
        public CsvConfiguration(string dataDir) => DataDir = dataDir;

        public string DataDir { get; }
        public string CardsFileName => "cards.csv";
        public string CharactersFileName => "characters.csv";
        public string ClassesFileName => "classes.csv";
        public string CharacterCardsFileName => "charcards.csv";
    }
}