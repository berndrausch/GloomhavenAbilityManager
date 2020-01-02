namespace GloomhavenAbilityManager.Logic.Data
{
    public class CharacterClass
    {
        public static readonly CharacterClass Default = new CharacterClass {Id = -1, Name = "Alle", NumberOfCards = 0};

        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfCards { get; set; }
    }
}