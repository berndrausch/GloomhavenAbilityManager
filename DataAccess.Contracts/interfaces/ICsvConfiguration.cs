namespace GloomhavenAbilityManager.DataAccess.Contracts.interfaces
{
    public interface ICsvConfiguration
    {
        string DataDir { get; }

        string CardsFileName { get; }

        string CharactersFileName { get; }

        string ClassesFileName { get; } 

        string CharacterCardsFileName { get; }
    }
}
