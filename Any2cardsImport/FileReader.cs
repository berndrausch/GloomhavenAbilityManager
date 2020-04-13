using System;
using System.Collections.Generic;
using System.IO;

namespace GloomhavenAbilityManager.Any2cardsImport
{
    public class FileReader
    {
        public IEnumerable<FileInfo> GetAllFiles(DirectoryInfo directory)
        {
            return directory.GetFiles("*.png", SearchOption.AllDirectories);
        }
    }
}
