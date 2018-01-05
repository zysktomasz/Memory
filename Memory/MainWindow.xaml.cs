﻿using System;
using System.Collections.Generic;
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
        private DispatcherTimer incorrectCardPairTimer;
        private DispatcherTimer startGameTimer;
        int timeInS = 0;
        bool firstClick = false;

        public MainWindow()
        {
            InitializeComponent();

            // utworzenie listy z dodanymi do projektu zdjeciami Card
            FileListExtract.FindAllFiles();

            // utworzenie listy dostepnych pol gry
            // TODO - automatyczne generowanie podanej ilosci pol
            List<Rectangle> RectangleList = new List<Rectangle>
            {
                rectangle_1, rectangle_2, rectangle_3, rectangle_4,
                rectangle_5, rectangle_6, rectangle_7, rectangle_8,
                rectangle_9, rectangle_10, rectangle_11, rectangle_12
            };

            // dodanie eventhandlerow dla kazdego pola
            foreach (var rec in RectangleList)
            {
                rec.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            }


            // generuje wszystkie pary karty dla dostepnych pol
            Card.GenerateAllCards(RectangleList);


            // tworzy timer (1,2 sekundowy) po wybraniu nieprawidlowej pary
            incorrectCardPairTimer = new DispatcherTimer();
            incorrectCardPairTimer.Tick += new EventHandler(incorrectCardPairTimer_Tick);
            incorrectCardPairTimer.Interval = new TimeSpan(0, 0, 0, 1, 200);

            // tworzy timer na rozpoczecie rozgrywki
            startGameTimer = new DispatcherTimer();
            startGameTimer.Interval = TimeSpan.FromSeconds(1);
            startGameTimer.Tick += new EventHandler(startGameTimer_Tick);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            przycisk.Content = "XDDDD";
            
        }
        
        // timer po wybraniu niepasujacej pary
        private void incorrectCardPairTimer_Tick(object sender, EventArgs e)
        {
            List<Card> clicked = Card.CardList.Where(element => element.IsClicked).ToList();
            foreach (var card in Card.CardList.Where(element => element.IsClicked).ToList())
            {
                card.CorrespondingRectangle.Fill = Brushes.AliceBlue;
                card.CorrespondingRectangle.IsEnabled = true;
                card.IsClicked = false;
            }

            // resetuje ilosc kliknietych
            UserConfig.ClickedCount = 0;
            // wylacza timer
            incorrectCardPairTimer.IsEnabled = false;
        }

        // timer po wybraniu niepasujacej pary
        private void startGameTimer_Tick(object sender, EventArgs e)
        {
            timeInS += 1;
            TimeSpan time = TimeSpan.FromSeconds(timeInS);
            string str = time.ToString(@"hh\:mm\:ss\:fff");
            labelTime.Content = str;
        }



        // utworzenie eventu
        void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // sprawdza czy to pierwsze klikniecie na pole (jesli tak, to odpala timer)
            if(firstClick == false)
            {
                firstClick = true;
                startGameTimer.Start();
            }
            // jesli mozna kliknac
            // TODO upewnic sie, ze nie klika sie 2 razy tego samego
            if (UserConfig.ClickedCount < 2)
            {
               
                Card clickedCard = Card.CardList.Find(card => card.CorrespondingRectangle == (Rectangle)sender);
                Rectangle R = clickedCard.CorrespondingRectangle;
                // wyswietla odpowiedni obraz w odpowiednim polu
                R.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(clickedCard.ImageUri)
                };
                // dezaktywuje mozliwosc ponownego klikniecia
                R.IsEnabled = false;

                // ustawia flage Karty, ze jest kliknieta
                clickedCard.IsClicked = true;

                // aktualizuje ilosc kliknietych
                UserConfig.ClickedCount++;
                
                // jesli klikniete sa 2 rozne pole
                if(UserConfig.ClickedCount == 2)
                {
                    // clicked - lista Card o fladze IsClicked == true
                    // ClickedCount wynosi 2, wiec wiadomo, ze lista ma zawierac 2 elementy na ktorych dzialamy
                    List<Card> clicked = Card.CardList.Where(element => element.IsClicked).ToList();

                    // jesli obydwa klikniete pola maja ten sam obraz
                    if(clicked[0].ImageUri == clicked[1].ImageUri)
                    {
                        przycisk.Content = "GIT";
                        // resetuje licznik aktualnie porownywach kart
                        UserConfig.ClickedCount = 0;

                        // sprawdza czy wygrano
                        if (UserConfig.CheckForWin())
                        {
                            startGameTimer.Stop();
                            MessageBox.Show("GRATULACJE, WYGRANA");
                        }

                        // zmienia flage IsClicked na false (karta nie jest aktywna)
                        clicked[0].IsClicked = false;
                        clicked[1].IsClicked = false;
                    }
                    // jesli wybrane karty sa rozne
                    else
                    {
                        przycisk.Content = "NIE ZGADZA SIE";

                        // uruchamia timer 2 sekundowy (czas na zapamietanie ulozenia kart)
                        incorrectCardPairTimer.Start();
                    }

                    
                    
                }
                
            }
            




        }
    }
}
