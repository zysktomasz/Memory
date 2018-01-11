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
        MemoryBoard board; 
        public static DispatcherTimer incorrectCardPairTimer;
        public static DispatcherTimer startGameTimer;
        bool firstClick = false; // flaga dla pierwszego klikniecia (do odpalenia timera rozgrywki dopiero po pierwszym kliknieciu)

        public MainWindow()
        {
            InitializeComponent();
            InitializeMemoryLayout();
        }

        private void InitializeMemoryLayout()
        {
            

            // utworzenie listy dostepnych pol gry
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

            // tworzy aktualnie rozgrywana plansze
            board = new MemoryBoard(RectangleList, list);
            gridMemoryBoard.DataContext = board; // udostepnia aktualna plansze do bindowania

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
            foreach (var card in board.ListOfCards.AllCards.Where(element => element.IsClicked).ToList())
            {
                card.CorrespondingRectangle.Fill = Brushes.AliceBlue;
                card.CorrespondingRectangle.IsEnabled = true;
                card.IsClicked = false;
            }
            board.ClickedCount = 0; // resetuje ilosc odkrytych kart
            incorrectCardPairTimer.IsEnabled = false; // wykonuje sie tylko raz
        }

        /// <summary>
        /// event timera rozpoczecia rozgrywki
        /// aktywuje sie po pierwszym klikniecu w ktorekolwiek pole
        /// </summary>
        private void startGameTimer_Tick(object sender, EventArgs e)
        {
            board.TimePassed += 1;
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
            board.RectangleClick((Rectangle)sender);
        }


        /// <summary>
        /// event - na klikniecie resetowania planszy
        /// </summary>
        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            // wylacza ewentualny timer chowania niepasujacej pary
            incorrectCardPairTimer.IsEnabled = false; 
            
            // resetujemy jakies globalne i statyczne zmienne
            startGameTimer.IsEnabled = false;
            startGameTimer.Stop();
            firstClick = false;

            InitializeMemoryLayout();
        }
    }
}
