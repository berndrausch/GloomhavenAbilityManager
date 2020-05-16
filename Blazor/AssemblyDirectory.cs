using System.IO;
using System.Reflection;

namespace GloomhavenAbilityManager.Blazor{
    public static class AssemblyDirectory
    {
        public static string Get() 
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
            return assemblyDirectory;
        }
    }
}