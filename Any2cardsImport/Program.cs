using System;
using System.IO;

namespace GloomhavenAbilityManager.Any2cardsImport
{
    class Program
    {
        static void Main(string[] args)
        {
            FileReader reader = new FileReader();
            var files = reader.GetAllFiles(new DirectoryInfo(@"C:\Users\Bernd\Documents\Development\gloomhaven\images\character-ability-cards"));
            
            Importer importer = new Importer();
            importer.Import(files);
            
            Console.WriteLine("Hello World!");
        }
    }
}
