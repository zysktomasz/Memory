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
        public string FileName { get; set; }
        public int Occurence { get; set; }

        public List<Card> CardList { get; set; }


        public Card(Rectangle correspondingRectangle, string fileName)
        {
            CorrespondingRectangle = correspondingRectangle;
            FileName = fileName;
            Occurence = 2;
        }

        public void GenerateAllCards(List<Rectangle> RectangleList)
        {

            int CardsToGenerate = RectangleList.Count / 2;

            // dziala dla kazdej pary
            for (int i = 0; i < CardsToGenerate; i++)
            {
                // pobiera losowe zdjecie dla pary
                Random rnd = new Random();
                rnd.Next(FileListExtract.FileList.Count - 1);

                // pobiera losowy rectangle (dwukrotnie)
                //rnd.Next(RectangleList.Count - 1);


                //CardList.Add()
            }
            

        }

    }
}
