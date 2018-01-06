using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Memory
{
    /// <summary>
    /// Interface umozliwiajacy generowanie listy obrazkow z roznych zrodel
    /// aktualnie dziedziczy z niego jedynie
    /// klasa BitmapImageListFromResources - tworzy liste na podstawie obrazkow z Resources
    /// </summary>
    interface IBitmapImageList
    {
        List<BitmapImage> BitmapImageList { get; set; }
        void GenerateBitmapImageList();
    }
}
