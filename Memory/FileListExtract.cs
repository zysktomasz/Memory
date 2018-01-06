using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Collections;

namespace Memory
{
    // klasa sluzy do utworzenia listy z nazwami plikow (zdjec)
    // na podstawie ktorej, klasa Card utworzy mozliwe pary
    class FileListExtract
    {
        public static List<string> FileList { get; set; }

        

        public static void FindAllFiles()
        {
            // uzyskanie listy Resource plikow projektu
            var asm = Assembly.GetExecutingAssembly();
            string resName = asm.GetName().Name + ".g.resources"; // Memory.g.resources zawiera wszystkie Resources
            using (var stream = asm.GetManifestResourceStream(resName)) // System.IO.UnmanagedMemoryStream
            using (var reader = new System.Resources.ResourceReader(stream)) // System.Resources.ResourceReader
            {
                FileList = reader.Cast<DictionaryEntry>().Where(entry => entry.Key.ToString().Contains(".png")).Select(entry => (string)entry.Key).ToList();
            }

        }
    }
}
