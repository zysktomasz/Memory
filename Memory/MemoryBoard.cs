using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Memory
{
    /// <summary>
    /// zawiera informacje o planszy dla aktualnej rozgrywki
    /// </summary>
    class MemoryBoard : INotifyPropertyChanged
    {

        public int ClickedCount { get; set; }
        public int RevealedCards { get; set; }
        public string TimePassedString { get; set; } = "00:00:00";
        public CardList ListOfCards { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        // przechowuje czas sekund od rozpoczecia rozgrywki
        private int timePassed;
        public int TimePassed
        {
            get { return timePassed; }
            set {
                timePassed = value;
                TimePassedString = TimeSpan.FromSeconds(value).ToString();
                OnPropertyChanged("TimePassedString"); // aktywuje event powiazany z labelem od czasu w UI
            }
        }

        // liczba wszystkich klikniec podczas danej rozgrywki
        private int clicks;
        public int Clicks
        {
            get { return clicks; }
            set
            {
                clicks = value;
                OnPropertyChanged("Clicks");
            }
        }


        // konstruktor
        public MemoryBoard(List<Rectangle> argRectangleList, IBitmapImageList arglista)
        {
            ListOfCards = new CardList(argRectangleList, arglista);
        }

        // utworzenie eventu
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public void RectangleClick(Rectangle clickedRectangle)
        {
            if (ClickedCount < 2)
            {
                Clicks++; // calkowita ilosc klikniec w pola

                // znajduje karte polaczona z wlasnie kliknietym polem (rectangle)
                Card clickedCard = ListOfCards.AllCards.Find(card => card.CorrespondingRectangle == clickedRectangle);
                // wyswietla odpowiedni obraz w odpowiednim polu
                clickedCard.CorrespondingRectangle.Fill = new ImageBrush
                {
                    ImageSource = clickedCard.CardImage
                };
                clickedCard.CorrespondingRectangle.IsEnabled = false; // dezaktywuje mozliwosc ponownego klikniecia
                clickedCard.IsClicked = true; // ustawia flage Karty, ze jest kliknieta
                ClickedCount++; // aktualizuje ilosc sprawdzanych kart

                // jesli sprawdzono dwie karty
                if (ClickedCount == 2)
                {
                    ValidateRectangleClicked();
                }
            }
        }

        /// <summary>
        /// metoda walidujaca rownowartosc obydwu sprawdzanych kart
        /// </summary>
        private void ValidateRectangleClicked()
        {
            // clicked - lista sprawdzanych kart (o fladze IsClicked)
            // ClickedCount wynosi 2, wiec wiadomo, ze lista ma zawierac 2 elementy na ktorych dzialamy
            List<Card> clicked = ListOfCards.AllCards.Where(element => element.IsClicked).ToList();

            // jesli obydwa klikniete pola maja ten sam obraz
            if (clicked[0].CardImage == clicked[1].CardImage)
            {
                ClickedCount = 0; // resetuje licznik aktualnie porownywach kart
                CheckForWin(); // sprawdza czy wygrano juz cala gre
                // zmienia flage IsClicked na false (karta nie jest juz sprawdzana)
                clicked[0].IsClicked = false;
                clicked[1].IsClicked = false;
            }
            // jesli wybrane karty sa rozne
            else
            {
                MainWindow.incorrectCardPairTimer.Start(); // uruchamia timer (czas na zapamietanie ulozenia kart)
            }
        }

        /// <summary>
        /// metoda sprawdza czy skonczono juz cala rozgrywke
        /// </summary>
        private void CheckForWin()
        {
            RevealedCards += 2; // aktualizuje licznik dobrze trafionych par
            // jesli juz wszystkie karte sa odkryte
            if (RevealedCards == ListOfCards.AllCards.Count)
            {
                MainWindow.startGameTimer.Stop();
                System.Windows.MessageBox.Show("GRATULACJE, WYGRANA");
            }
        }
    }
}
