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
    abstract class FileListExtract
    {
        public static List<string> FileList { get; set; }
        public static string[] testowaLista { get; set; }

        

        public static void FindAllFiles()
        {


            // uzyskanie Assembly aktualnie dzialajacej aplikacji (Memory)
            Assembly _assembly;
            _assembly = Assembly.GetExecutingAssembly();

            // utworzenie listy dodanych zewnetrznych plikow (embedded resources)
            // z uzyciem lambdy, do odfiltrowania jedynie zdjec (.png)

            //List<string> resources = new List<string>(
            //    _assembly.GetManifestResourceNames()
            //    .Where(arg => 
            //    arg.ToString().Contains(".png"))
            //    );


            // uzyskanie listy Resource plikow projektu
            var asm = Assembly.GetExecutingAssembly();
            string resName = asm.GetName().Name + ".g.resources"; // Memory.g.resources zawiera wszystkie Resources
            using (var stream = asm.GetManifestResourceStream(resName)) // System.IO.UnmanagedMemoryStream
            using (var reader = new System.Resources.ResourceReader(stream)) // System.Resources.ResourceReader
            {
                //testowaLista = reader.Cast<DictionaryEntry>().Where(entry => entry.Key.ToString().Contains(".png")).Select(entry => (string)entry.Key).ToArray();

                FileList = reader.Cast<DictionaryEntry>().Where(entry => entry.Key.ToString().Contains(".png")).Select(entry => (string)entry.Key).ToList();
                //FileList = testowaLista.ToList();
            }



            //for(int i = 0; i < resources.Count; i++)
            //{
            //    resources[i] = resources[i].Replace("Memory.img.", "/");
            //}
            //FileList = resources;


        }
    }
}
