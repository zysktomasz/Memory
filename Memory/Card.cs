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
    /// oraz udostepnia statyczna liste aktywnych kart dla wszystkich pol
    /// </summary>
    class Card
    {
        public Rectangle CorrespondingRectangle { get; set; } // obiekt w WPF odpowiadajacy obiektowi Karty
        public BitmapImage CardImage { get; set; }
        public bool IsClicked { get; set; }
        static Random rnd = new Random();

        public static List<Card> CardList = new List<Card>();

        private Card(Rectangle correspondingRectangle, BitmapImage cardImage)
        {
            CorrespondingRectangle = correspondingRectangle;
            CardImage = cardImage;
            IsClicked = false;
        }

        /// <summary>
        /// metoda GenerateAllCards
        /// generuje liste kart dla wszystkich pol
        /// laczac obiekty Rectangle z UI WPF oraz liste obrazkow tworzonych przez klase spod IBitmapImageList
        /// </summary>
        public static void GenerateAllCards(List<Rectangle> RectangleList, IBitmapImageList lista)
        {
            // lista gotowych obrazkow
            List<BitmapImage> list = lista.BitmapImageList;

            int CardsToGenerate = RectangleList.Count / 2; // generujemy jedna karte dla dwoch pol (pary)


            for (int i = 0; i < CardsToGenerate; i++)
            {
                // pobiera losowe zdjecie dla pary
                int chosenImage = rnd.Next(list.Count);
                BitmapImage tempBitmapImage = list[chosenImage];
                list.RemoveAt(chosenImage); // usuwa wybrane zdjecie (zeby go nie uzyc ponownie)

                // tworzy dwie Karty o tym samym obrazku dla dwoch roznych pol
                for (int j = 0; j < 2; j++)
                {
                    int chosenRectangle = rnd.Next(RectangleList.Count - 1);
                    CardList.Add(new Card(RectangleList[chosenRectangle], tempBitmapImage));
                    RectangleList.RemoveAt(chosenRectangle); // usuwa wybrany rectangle (zeby go nie uzyc ponownie)
                }
            }
        }
    }
}
