namespace GloomhavenAbilityManager.Any2cardsImport
{
    public class AdditionalCardInfo
    {
        public string FileName{get;set;}
        
        public int Id{get;set;}
        
        public string Name{get;set;}
        
        public string Level{get;set;}

        public override string ToString()
        {
            return $"{nameof(AdditionalCardInfo)} for {Name} ({Level}, {Id}";
        }
    }
}