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
using MultiServiceBrowser.src;
using System.ComponentModel;
using Microsoft.Win32;
using System.Windows.Threading;

namespace MultiServiceBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        private BrowserFixes _browserFixes;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            _browserFixes = new BrowserFixes();
            _browserFixes.SetWebBrowserFeatures();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            onLoadParseConfig();
        }

        private void NewImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Image clickedButton = sender as Image;

                int indexValue = int.Parse(clickedButton.Tag.ToString());
                if(clickedButton.Tag != null)
                {
                    uiFrame.Source = new Uri(configuration.listSites[indexValue].url, UriKind.RelativeOrAbsolute);
                    uiFrame.NavigationService.RemoveBackEntry();
                    //WebBrowser uiBrowser = new WebBrowser();
                    //uiBrowser.Name = "eleBrowser";
                    //uiBrowser.Visibility = Visibility.Visible;
                    //uiGrid.Children.Add(uiBrowser);
                    //uiBrowser.Navigate(configuration.listSites[indexValue].url);   
                }
            }
            catch
            {

            }
            
        }

        private void onLoadParseConfig()
        {
            try
            {
                configuration _config = new configuration();
                _config.LoadFile();

                for (int i = 0; i < configuration.listSites.Count; i++)
                {

                    StackPanel newStackPanel = new StackPanel();
                    newStackPanel.Orientation = Orientation.Horizontal;


                    //Canvas gridImages = new Canvas();

                    Image newImage = MakeSiteIcon(i);
                    newStackPanel.Children.Add(newImage);
                    //gridImages.Children.Add(newImage);

                    //Ellipse newEllipse = MakeSiteIconAcivity(i);
                    //gridImages.Children.Add(newEllipse);

                    //add the icon to NewStackPanel
                    //newStackPanel.Children.Add(gridImages);

                    //Ading a new stackpanel for the lables to sit correctly
                    StackPanel stackLabels = new StackPanel();

                    //add the label to stackLabels
                    Label newLabel = MakeSiteLabelName(i);                                 
                    stackLabels.Children.Add(newLabel);

                    //add the label to stackLabels
                    Label labelHost = MakeSiteLabelHost(i);
                    stackLabels.Children.Add(labelHost);

                    //add label to the NewStackPanel
                    newStackPanel.Children.Add(stackLabels);

                    //adds to the core menu
                    SiteIcons.Children.Insert(SiteIcons.Children.Count - 2, newStackPanel);
                }

            }
            catch
            {

            }
        }

        private Image MakeSiteIcon(int i)
        {
            try
            {
                Image newImage = new Image();

                if (configuration.listSites[i].iconPath == null ||
                    configuration.listSites[i].iconPath == "")
                {
                    newImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Resources\\Help_64px.png"));

                }
                else
                {
                    newImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + $"\\Resources\\{configuration.listSites[i].iconPath}"));
                }

                newImage.Name = $"Link{i}".ToString();
                newImage.Width = 58;
                newImage.Height = 58;
                newImage.Margin = new Thickness(4, 0, 4, 0);
                newImage.Tag = i;
                newImage.HorizontalAlignment = HorizontalAlignment.Center;
                newImage.MouseUp += NewImage_MouseUp;

                return newImage;
            }
            catch
            {
                return null;
            }
        }

        private Ellipse MakeSiteIconAcivity(int i)
        {
            try
            {
                Ellipse newEllipse = new Ellipse();
                SolidColorBrush brushRed = new SolidColorBrush();
                brushRed.Color = Colors.Red;
                newEllipse.Fill = brushRed;
                newEllipse.Width = 10;
                newEllipse.Height = 10;
                newEllipse.Name = $"Link{1}Color".ToString();

                return newEllipse;
            }
            catch
            {
                return null;
            }
        }

        private Label MakeSiteLabelName(int i)
        {
            try
            {
                Label newLabel = new Label();
                newLabel.VerticalAlignment = VerticalAlignment.Center;
                newLabel.FontSize = 18;
                newLabel.Content = configuration.listSites[i].site;

                return newLabel;
            }
            catch
            {
                return null;
            }
        }

        private Label MakeSiteLabelHost(int i)
        {
            try
            {
                Label labelHost = new Label();
                labelHost.VerticalAlignment = VerticalAlignment.Center;
                labelHost.FontSize = 10;
                labelHost.Content = configuration.listSites[i].host;

                return labelHost;
            }
            catch
            {
                return null;
            }
        }

        private void ReloadSites()
        {
            try
            {
                Image MenuRootHamburger = new Image();
                MenuRootHamburger.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Resources\\Menu_64px.png"));
                MenuRootHamburger.Name = "MenuRootHamburger";
                MenuRootHamburger.Width = 64;
                MenuRootHamburger.Height = 64;
                MenuRootHamburger.Tag = "MenuRootHamburger";
                MenuRootHamburger.HorizontalAlignment = HorizontalAlignment.Left;
                MenuRootHamburger.MouseUp += MenuRootHamburger_MouseUp;

                SiteIcons.Children.Insert(0, MenuRootHamburger);

                Image MenuRootAdd = new Image();
                MenuRootAdd.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Resources\\Plus_64px.png"));
                MenuRootAdd.Name = "MenuRootAdd";
                MenuRootAdd.Width = 64;
                MenuRootAdd.Height = 64;
                MenuRootAdd.Tag = "MenuRootAdd";
                MenuRootAdd.HorizontalAlignment = HorizontalAlignment.Left;
                MenuRootAdd.MouseUp += MenuRootAdd_MouseUp;

                SiteIcons.Children.Insert(1, MenuRootAdd);

                
                Image MenuRootAbout = new Image();
                MenuRootAbout.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Resources\\Help_64px.png"));
                MenuRootAbout.Name = "MenuRootAbout";
                MenuRootAbout.Width = 64;
                MenuRootAbout.Height = 64;
                MenuRootAbout.Tag = "MenuRootAbout";
                MenuRootAbout.HorizontalAlignment = HorizontalAlignment.Left;
                MenuRootAbout.MouseUp += MenuRootAbout_MouseUp;

                SiteIcons.Children.Insert(2, MenuRootAbout);

                onLoadParseConfig();
            }
            catch
            {

            }
        }

        private void MenuRootHamburger_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                uiSplitView.IsPaneOpen = !uiSplitView.IsPaneOpen;

            }
            catch
            {

            }
        }

        private void MenuRootHamburger_Loaded(object sender, RoutedEventArgs e)
        {
            // ... Create a new BitmapImage.
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string t = Directory.GetCurrentDirectory() + "//Resources//menu_64px.png";
            b.UriSource = new Uri(t);
            b.EndInit();

            // ... Get Image reference from sender.
            var image = sender as Image;
            // ... Assign Source.
            image.Source = b;
        }

        private async void MenuRootAdd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                await Task.Delay(1);
                //uiNewSite _uiNewSite = new uiNewSite();
                //_uiNewSite.ShowDialog();
                uiFrame.Content = new PageNewSite();

                //SiteIcons.Children.Clear();
                //clear the sites and reparse the config
                //ReloadSites();

            }
            catch
            {

            }
        }

        private void MenuRootAdd_Loaded(object sender, RoutedEventArgs e)
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

        private void MenuRootAbout_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //about screen
            //uiGrid.Children.Add()
            uiFrame.Content = new PageAbout();

        }

        private void MenuRootAbout_Loaded(object sender, RoutedEventArgs e)
        {
            // ... Create a new BitmapImage.
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            string t = Directory.GetCurrentDirectory() + "//Resources//Info_64px.png";
            b.UriSource = new Uri(t);
            b.EndInit();

            // ... Get Image reference from sender.
            var image = sender as Image;
            // ... Assign Source.
            image.Source = b;
        }

    }
}
