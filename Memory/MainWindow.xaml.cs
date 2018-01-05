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

            //List<Rectangle> RectangleList = new List<Rectangle>
            //{
            //    rectangle_1, rectangle_2, rectangle_3, rectangle_4, rectangle_5,rectangle_6,rectangle_7,rectangle_8,
            //    rectangle_9,
            //    rectangle_10,rectangle_11,rectangle_12
            //};

            // dodanie eventow do eventhandlera

            rectangle_1.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            rectangle_2.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            rectangle_3.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
            przycisk.Content = "XDDDD";
            
        }
        
        
        // utworzenie eventu
        void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // test czy dziala klikniecie w rectangle
            Rectangle R = (Rectangle)sender;
            

            // DZIALA DLA Resource OBRAZOW
            R.Fill = new ImageBrush
            {
                //ImageSource = new BitmapImage(new Uri(@"img\cat_1.png", UriKind.RelativeOrAbsolute))
                //ImageSource = new BitmapImage(new Uri(
                //                    "pack://application:,,,/img/1_fish.png"))

                ImageSource = new BitmapImage(new Uri(String.Format("pack://application:,,,/{0}", FileListExtract.FileList[10])
                                    ))
            };

            
            
            
            


        }
    }
}
