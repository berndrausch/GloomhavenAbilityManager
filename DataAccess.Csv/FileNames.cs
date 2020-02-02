using System.IO;

namespace GloomhavenAbilityManager.DataAccess.Csv
{
    internal static class FileNames
    {
        private static string BaseDirectory = "DataAccess.Csv";

        internal static string Cards => Path.Combine(BaseDirectory, "cards.csv");
        internal static string Characters => Path.Combine(BaseDirectory, "characters.csv");
        internal static string Classes => Path.Combine(BaseDirectory, "classes.csv");
        internal static string CharacterCards => Path.Combine(BaseDirectory, "charcards.csv");
    }
}