using MahApps.Metro.Controls;
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

namespace MultiServiceBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void onLoadParseConfig()
        {
            try
            {
                Image img = new Image();
                //img.Source = new BitmapImage(new Uri());
                img.Width = 50;
                img.Height = 50;
                //img.HorizontalAlignment = Left;
                img.Focusable = true;
                //img.MouseUp +=

                SiteIcons.Children.Insert(SiteIcons.Children.Count - 2, img);
            }
            catch
            {

            }
        }

        private void MenuRoot00_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                uiNewSite _uiNewSite = new uiNewSite();
                _uiNewSite.ShowDialog();
                //uiBrowser.Navigate("http://www.google.com");
                //uiBrowser.Visibility = Visibility.Visible;
            }
            catch
            {

            }
        }

        private void MenuRoot00_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // ... Create a new BitmapImage.
                BitmapImage b = new BitmapImage();
                b.BeginInit();
                string t = Directory.GetCurrentDirectory() + "//Resources//plus_64px.png";
                b.UriSource = new Uri(t);
                b.EndInit();

                // ... Get Image reference from sender.
                var image = sender as Image;
                // ... Assign Source.
                image.Source = b;
            }
            catch
            {
                //MenuRoot02.Source = "null"; 
            }
        }


        private void MenuRoot01_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void MenuRoot01_Loaded(object sender, RoutedEventArgs e)
        {
            // ... Create a new BitmapImage.
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string t = Directory.GetCurrentDirectory() + "//Resources//info_64px.png";
            b.UriSource = new Uri(t);
            b.EndInit();

            // ... Get Image reference from sender.
            var image = sender as Image;
            // ... Assign Source.
            image.Source = b;
        }


    }
}
