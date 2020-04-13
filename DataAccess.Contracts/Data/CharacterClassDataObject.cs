namespace GloomhavenAbilityManager.DataAccess.Contracts.Data
{
    public class CharacterClassDataObject
    {
        public static readonly CharacterClassDataObject Default = new CharacterClassDataObject {Id = -1, Name = "Alle", NumberOfCards = 0};

        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfCards { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name} {Name} with id {Id}";
        }
    }
}