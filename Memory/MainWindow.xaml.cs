using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace Memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // utworzenie tablicy z dodanymi do projektu zdjeciami Kart
            FileListExtract.FindAllFiles();

            List<Rectangle> RectangleList = new List<Rectangle>
            {
                rectangle_1, rectangle_2, rectangle_3, rectangle_4, rectangle_5,rectangle_6,rectangle_7,rectangle_8,
                rectangle_9,
                rectangle_10,rectangle_11,rectangle_12
            };

            // dodanie eventow do eventhandlera
            foreach (var rec in RectangleList)
            {
                rec.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            }


            // generuje wszystkie pary karty
            Card.GenerateAllCards(RectangleList); // dla 12 wolnych pol

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
            przycisk.Content = "XDDDD";
            
        }
        
        
        // utworzenie eventu
        void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Card clickedCard = Card.GetCardByRectangle((Rectangle)sender);

            // test czy dziala klikniecie w rectangle
            Rectangle R = (Rectangle)sender;



            clickedCard.CorrespondingRectangle.Fill = new ImageBrush
            {
                //ImageSource = new BitmapImage(new Uri(@"img\cat_1.png", UriKind.RelativeOrAbsolute))
                //ImageSource = new BitmapImage(new Uri(
                //                    "pack://application:,,,/img/1_fish.png"))

                ImageSource = new BitmapImage(clickedCard.ImageUri)
            };

            // DZIALA DLA Resource OBRAZOW
            //R.Fill = new ImageBrush
            //{
            //    //ImageSource = new BitmapImage(new Uri(@"img\cat_1.png", UriKind.RelativeOrAbsolute))
            //    //ImageSource = new BitmapImage(new Uri(
            //    //                    "pack://application:,,,/img/1_fish.png"))

            //    ImageSource = new BitmapImage(new Uri(String.Format("pack://application:,,,/{0}", FileListExtract.FileList[10])
            //                        ))
            //};







        }
    }
}
