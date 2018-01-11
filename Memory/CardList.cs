using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Memory
{
    class CardList
    {
        static Random rnd = new Random();
        public List<Card> AllCards { get; set; }
        private List<Rectangle> RectangleList;
        private IBitmapImageList lista;


        public CardList(List<Rectangle> argRectangleList, IBitmapImageList arglista)
        {
            AllCards = new List<Card>();
            this.RectangleList = argRectangleList;
            this.lista = arglista;
            GenerateCardList();
        }

        /// <summary>
        /// metoda GenerateCardList
        /// generuje liste kart dla wszystkich pol
        /// laczac obiekty Rectangle z UI WPF oraz liste obrazkow tworzonych przez klase spod IBitmapImageList
        /// </summary>
        private void GenerateCardList()
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
                    AllCards.Add(new Card(RectangleList[chosenRectangle], tempBitmapImage));
                    RectangleList.RemoveAt(chosenRectangle); // usuwa wybrany rectangle (zeby go nie uzyc ponownie)
                }
            }
        }
    }
}
