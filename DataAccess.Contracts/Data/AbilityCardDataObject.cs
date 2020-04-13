namespace GloomhavenAbilityManager.DataAccess.Contracts.Data
{
    public class AbilityCardDataObject
    {
        public int Id { get; set; }

        public int ClassId { get; set; }

        public string Level { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }
    }
}