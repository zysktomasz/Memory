using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer incorrectCardPairTimer;
        DispatcherTimer startGameTimer;
        int timeInS = 0; // czas trwania aktualnej rozgrywki
        bool firstClick = false; // flaga dla pierwszego klikniecia (do odpalenia timera rozgrywki dopiero po pierwszym kliknieciu)

        public MainWindow()
        {
            InitializeComponent();
            InitializeMemoryLayout();
        }

        private void InitializeMemoryLayout()
        {
            // utworzenie listy dostepnych pol gry
            // aktualnie jest to stala ilosc :|
            // TODO - automatyczne generowanie podanej ilosci pol
            List<Rectangle> RectangleList = new List<Rectangle>
            {
                rectangle_1, rectangle_2, rectangle_3, rectangle_4,
                rectangle_5, rectangle_6, rectangle_7, rectangle_8,
                rectangle_9, rectangle_10, rectangle_11, rectangle_12,
                rectangle_13, rectangle_14, rectangle_15, rectangle_16,
                rectangle_17, rectangle_18, rectangle_19, rectangle_20,
                rectangle_21, rectangle_22, rectangle_23, rectangle_24
            };

            // "resetuje" wszystkie pola (ustawia tlo na kolor, i wlacza mozliwosc klikniecia)
            foreach (var rec in RectangleList)
            {
                rec.Fill = Brushes.AliceBlue;
                rec.IsEnabled = true;
            }

            // w przyszlosci mozna by ustawic np pobieranie obrazkow do Kart z roznych zrodel
            // aktualnie dostepnie jest jedynie pobieranie z Resources
            IBitmapImageList list = new BitmapImageListFromResources();

            // generuje wszystkie karty dla dostepnych pol
            Card.GenerateAllCards(RectangleList, list);


            // tworzy timer (0,5 sekundowy) po wybraniu nieprawidlowej pary
            incorrectCardPairTimer = new DispatcherTimer();
            incorrectCardPairTimer.Tick += new EventHandler(IncorrectCardPairTimer_Tick);
            incorrectCardPairTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);

            // tworzy timer na rozpoczecie rozgrywki
            startGameTimer = new DispatcherTimer();
            startGameTimer.Interval = TimeSpan.FromSeconds(1);
            startGameTimer.Tick += new EventHandler(startGameTimer_Tick);
        }

        /// <summary>
        /// event - po 500ms chowa odkryta, niepasujaca pare
        /// </summary>
        private void IncorrectCardPairTimer_Tick(object sender, EventArgs e)
        {
            HidenWrongCardPair();
            incorrectCardPairTimer.IsEnabled = false; // wykonuje sie tylko raz
        }

        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        private void HidenWrongCardPair()
        {
            foreach (var card in Card.CardList.Where(element => element.IsClicked).ToList())
            {
                card.CorrespondingRectangle.Fill = Brushes.AliceBlue;
                card.CorrespondingRectangle.IsEnabled = true;
                card.IsClicked = false;
            }

            // resetuje ilosc odkrytych kart
            UserConfig.ClickedCount = 0;
        }

        /// <summary>
        /// event timera rozpoczecia rozgrywki
        /// aktywuje sie po pierwszym klikniecu w ktorekolwiek pole
        /// </summary>
        private void startGameTimer_Tick(object sender, EventArgs e)
        {
            timeInS += 1;
            TimeSpan time = TimeSpan.FromSeconds(timeInS);
            string str = time.ToString();
            labelTime.Content = str; // kiedy nie wiesz jak bindowac ;|
        }



        /// <summary>
        /// event - aktywowaniu po kliknieciu kazdego aktywnego(nieodkrytego) pola
        /// </summary>
        void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // sprawdza czy to pierwsze klikniecie na pole (false oznacza, ze nie bylo jeszcze pierwszego klikniecia)
            if(firstClick == false)
            {
                firstClick = true;
                startGameTimer.Start();
            }

            // w jednym czasie mozna porownywac maksymalnie 2 karty (pare)
            // wiec odkryto 1 lub 0 kart, to mozna odkryc kolejna
            if (UserConfig.ClickedCount < 2)
            {
                UserConfig.Clicks++; // calkowita ilosc klikniec w pola
                labelClicks.Content = UserConfig.Clicks; // todo binding

                // znajduje karte polaczona z wlasnie kliknietym polem (rectangle)
                Card clickedCard = Card.CardList.Find(card => card.CorrespondingRectangle == (Rectangle)sender);
                // wyswietla odpowiedni obraz w odpowiednim polu
                clickedCard.CorrespondingRectangle.Fill = new ImageBrush
                {
                    ImageSource = clickedCard.CardImage
                };
                clickedCard.CorrespondingRectangle.IsEnabled = false; // dezaktywuje mozliwosc ponownego klikniecia
                clickedCard.IsClicked = true; // ustawia flage Karty, ze jest kliknieta
                UserConfig.ClickedCount++; // aktualizuje ilosc sprawdzanych kart

                // jesli sprawdzono dwie karty
                if (UserConfig.ClickedCount == 2)
                {
                    ClickedCardsValidate();
                }
            }
        }

        /// <summary>
        /// metoda walidujaca rownowartosc obydwu sprawdzanych kart
        /// </summary>
        private void ClickedCardsValidate()
        {
            // clicked - lista sprawdzanych kart (o fladze IsClicked)
            // UserConfig.ClickedCount wynosi 2, wiec wiadomo, ze lista ma zawierac 2 elementy na ktorych dzialamy
            List<Card> clicked = Card.CardList.Where(element => element.IsClicked).ToList();

            // jesli obydwa klikniete pola maja ten sam obraz
            if (clicked[0].CardImage == clicked[1].CardImage)
            {
                UserConfig.ClickedCount = 0; // resetuje licznik aktualnie porownywach kart
                CheckForWin(); // sprawdza czy wygrano juz cala gre
                // zmienia flage IsClicked na false (karta nie jest juz sprawdzana)
                clicked[0].IsClicked = false;
                clicked[1].IsClicked = false;
            }
            // jesli wybrane karty sa rozne
            else
            {
                incorrectCardPairTimer.Start(); // uruchamia timer (czas na zapamietanie ulozenia kart)
            }
        }

        /// <summary>
        /// metoda sprawdza czy skonczono juz cala rozgrywke
        /// </summary>
        private void CheckForWin()
        {
            UserConfig.RevealedCards += 2; // aktualizuje licznik dobrze trafionych par
            // jesli juz wszystkie karte sa odkryte
            if (UserConfig.RevealedCards == Card.CardList.Count)
            {
                startGameTimer.Stop();
                MessageBox.Show("GRATULACJE, WYGRANA");
            }
        }

        /// <summary>
        /// event - na klikniecie resetowania planszy
        /// </summary>
        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            // zeby nie zbugowac - jesli resetujemy w trakcie oczekiwania na schowanie sprawdzanej, niegodnej pary, to zatrzymujemy odliczanie
            // i sami uzywamy metody ukrywajacej
            incorrectCardPairTimer.IsEnabled = false; 
            HidenWrongCardPair();
            
            // resetujemy jakies globalne i statyczne zmienne
            startGameTimer.IsEnabled = false;
            timeInS = 0;
            firstClick = false;
            UserConfig.ClickedCount = 0;
            UserConfig.RevealedCards = 0;
            UserConfig.Clicks = 0;
            labelClicks.Content = UserConfig.Clicks; // bind :(
            Card.CardList.Clear();
            startGameTimer.Stop();
            labelTime.Content = "00:00:00"; // bind :(
            InitializeMemoryLayout();
        }
    }
}
