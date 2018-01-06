using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Memory
{
    abstract class BitmapImageListFromResources : IBitmapImageList
    {
        public List<BitmapImage> GenerateBitmapImageList()
        {
            // lista przechowujaca nazwy plikow
            List<string> FileList = new List<string>();
            // uzyskanie listy Resource plikow projektu
            var asm = Assembly.GetExecutingAssembly();
            string resName = asm.GetName().Name + ".g.resources"; // Memory.g.resources zawiera wszystkie Resources
            using (var stream = asm.GetManifestResourceStream(resName)) // System.IO.UnmanagedMemoryStream
            using (var reader = new System.Resources.ResourceReader(stream)) // System.Resources.ResourceReader
            {
                FileList = reader.Cast<DictionaryEntry>().Where(entry => entry.Key.ToString().Contains(".png")).Select(entry => (string)entry.Key).ToList();
            }

            // dla kazdego pliku tworzy obraz BitmapImage
            foreach (var file in FileList)
            {
                Uri tempUri = new Uri((String.Format("pack://application:,,,/{0}", file)));
            }
            
        }
    }
}
