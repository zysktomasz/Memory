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
        public int Occurence { get; set; }

        public static List<Card> CardList = new List<Card>();


        public Card(Rectangle correspondingRectangle, Uri imageUri)
        {
            CorrespondingRectangle = correspondingRectangle;
            ImageUri = imageUri;
            Occurence = 2;
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


                //pobiera losowy rectangle (dwukrotnie)
                int chosenRectangle = rnd.Next(RectangleList.Count - 1);
                Card temp = new Card(RectangleList[chosenRectangle], tempUri);
                RectangleList.RemoveAt(chosenRectangle);

                chosenRectangle = rnd.Next(RectangleList.Count - 1);
                temp = new Card(RectangleList[chosenRectangle], tempUri);
                RectangleList.RemoveAt(chosenRectangle);


                //CardList.Add()
            }




        }

        public static Card GetCardByRectangle(Rectangle rectangle)
        {
            Card temp = CardList.Find(element => element.CorrespondingRectangle == rectangle);
            return temp;
        }

    }
}
