namespace GloomhavenAbilityManager.Any2cardsImport
{
    public class AdditionalClassInfo
    {
        public string Abbreviation{get;set;}
        public int Id{get;set;}
        
        public string Name{get;set;}
        
        public int NumberOfCards{get;set;}

        public override string ToString()
        {
            return $"{nameof(AdditionalClassInfo)} for {Name} ({Abbreviation}, {Id}";
        }
    }
}