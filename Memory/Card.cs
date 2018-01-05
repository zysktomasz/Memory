using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Memory
{
    class Card
    {
        public Rectangle CorrespondingRectangle { get; set; }
        public Uri ImageUri { get; set; }
        public bool IsClicked { get; set; }

        public static List<Card> CardList = new List<Card>();

        public Card(Rectangle correspondingRectangle, Uri imageUri)
        {
            CorrespondingRectangle = correspondingRectangle;
            ImageUri = imageUri;
            IsClicked = false;
            CardList.Add(this);
        }

        public static void GenerateAllCards(List<Rectangle> RectangleList)
        {

            int CardsToGenerate = RectangleList.Count / 2;

            // dziala dla kazdej pary
            for (int i = 0; i < CardsToGenerate; i++)
            {
                // pobiera losowe zdjecie dla pary
                Random rnd = new Random();
                int chosenImage = rnd.Next(FileListExtract.FileList.Count - 1);
                Uri tempUri = new Uri((String.Format("pack://application:,,,/{0}", FileListExtract.FileList[chosenImage])));
                // usuwa wybrane zdjecie (zeby go nie uzyc ponownie)
                FileListExtract.FileList.RemoveAt(chosenImage);


                // pobiera losowy rectangle i tworzy Card o wybranym wyzej Uri
                int chosenRectangle = rnd.Next(RectangleList.Count - 1);
                Card temp = new Card(RectangleList[chosenRectangle], tempUri);
                // usuwa wybrany rectangle (zeby go nie uzyc ponownie)
                RectangleList.RemoveAt(chosenRectangle);

                // pobiera kolejny, losowy rectangle i tworzy Card o tym samym Uri co poprzedni (otrzymujemy pare pol o tych samych obiektach)
                chosenRectangle = rnd.Next(RectangleList.Count - 1);
                temp = new Card(RectangleList[chosenRectangle], tempUri);
                // usuwa wybrany rectangle (zeby go nie uzyc ponownie)
                RectangleList.RemoveAt(chosenRectangle);

            }




        }


    }
}
