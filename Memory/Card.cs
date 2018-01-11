using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Memory
{
    /// <summary>
    /// klasa Card tworzy obiekty skladajace sie z:
    /// obrazka (BitmapImage), flagi IsClicked, obiektu Rectangle (pola UI odpowiadajacemu danej karcie)
    /// </summary>
    class Card
    {
        public Rectangle CorrespondingRectangle { get; set; } // obiekt w WPF odpowiadajacy obiektowi Karty
        public BitmapImage CardImage { get; set; }
        public bool IsClicked { get; set; }
        

        public Card(Rectangle correspondingRectangle, BitmapImage cardImage)
        {
            CorrespondingRectangle = correspondingRectangle;
            CardImage = cardImage;
            IsClicked = false;
        }


    }
}
