using GloomhavenAbilityManager.DataAccess.Contracts.interfaces;

namespace GloomhavenAbilityManager.DataAccess.Csv
{
    public class CsvConfigurationProduction : ICsvConfiguration
    {
        public string DataDir => "DataAccess.Csv";
        public string CardsFileName => "cards.csv";
        public string CharactersFileName => "characters.csv";
        public string ClassesFileName => "classes.csv";
        public string CharacterCardsFileName => "charcards.csv";
    }
}