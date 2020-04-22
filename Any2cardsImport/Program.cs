using System;
using System.IO;

namespace GloomhavenAbilityManager.Any2cardsImport
{
    class Program
    {
        static void Main(string[] args)
        {
            string characterAbilityCardsDir = @"..\..\..\..\..\gloomhaven\images\character-ability-cards";

            if (args.Length > 0)
            {
                characterAbilityCardsDir = args[0];
            }

            FileReader reader = new FileReader();
            var files = reader.GetAllFiles(new DirectoryInfo(characterAbilityCardsDir));
            
            DataGenerator dataGenerator = new DataGenerator();
            dataGenerator.Generate(files, true);
            
            Console.WriteLine("Hello World!");
        }
    }
}
