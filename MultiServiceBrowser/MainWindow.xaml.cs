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
using MultiServiceBrowser.src.ui;
using System.ComponentModel;
using Microsoft.Win32;

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
            SetWebBrowserFeatures();
        }

        // set WebBrowser features, more info: http://stackoverflow.com/a/18333982/1768303
        static void SetWebBrowserFeatures()
        {
            // don't change the registry if running in-proc inside Visual Studio
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime)
                return;

            var appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            var featureControlRegKey = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\";

            Registry.SetValue(featureControlRegKey + "FEATURE_BROWSER_EMULATION",
                appName, GetBrowserEmulationMode(), RegistryValueKind.DWord);

            // enable the features which are "On" for the full Internet Explorer browser

            Registry.SetValue(featureControlRegKey + "FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION",
                appName, 1, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_AJAX_CONNECTIONEVENTS",
                appName, 1, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_GPU_RENDERING",
                appName, 1, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_WEBOC_DOCUMENT_ZOOM",
                appName, 1, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_NINPUT_LEGACYMODE",
                appName, 0, RegistryValueKind.DWord);
        }

        static UInt32 GetBrowserEmulationMode()
        {
            int browserVersion = 0;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }

            if (browserVersion < 7)
            {
                throw new ApplicationException("Unsupported version of Microsoft Internet Explorer!");
            }

            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode. 

            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. 
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. 
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.                    
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10.
                    break;
            }

            return mode;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            onLoadParseConfig();
            //onLoadTest();
        }

        private void onLoadTest()
        {
            try
            {
                configuration _config = new configuration();
                _config.LoadFile();

                for (int i = 0; i < 15; i++)
                {
                    Image newImage = new Image();
                    newImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Resources\\Help_64px.png"));
                    newImage.Name = $"Link{i}".ToString();
                    newImage.Width = 64;
                    newImage.Height = 64;
                    newImage.Tag = i;
                    newImage.HorizontalAlignment = HorizontalAlignment.Left;
                    newImage.MouseUp += NewImage_MouseUp;

                    var total = SiteIcons.Children.Count;

                    SiteIcons.Children.Insert(SiteIcons.Children.Count - 2, newImage);
                }
            }
            catch
            {

            }
        }

        private void NewImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Image clickedButton = sender as Image;

                int indexValue = int.Parse(clickedButton.Tag.ToString());
                if(clickedButton.Tag != null)
                {
                    

                    using (WebBrowser uiBrowser = new WebBrowser())
                    {
                        uiBrowser.Name = "eleBrowser";
                        uiBrowser.Visibility = Visibility.Visible;
                        uiGrid.Children.Add(uiBrowser);
                        uiBrowser.Navigate(configuration.listSites[indexValue].url);
                    }    
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

                    Image newImage = new Image();
                    newImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Resources\\Help_64px.png"));
                    newImage.Name = $"Link{i}".ToString();
                    newImage.Width = 64;
                    newImage.Height = 64;
                    newImage.Tag = i;
                    newImage.HorizontalAlignment = HorizontalAlignment.Left;
                    newImage.MouseUp += NewImage_MouseUp;

                    var total = SiteIcons.Children.Count;

                    SiteIcons.Children.Insert(SiteIcons.Children.Count - 2, newImage);
                }

            }
            catch
            {

            }
        }

        private void ReloadSites()
        {
            try
            {
                Image MenuRoot00 = new Image();
                MenuRoot00.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Resources\\Help_64px.png"));
                MenuRoot00.Name = "MenuRoot00";
                MenuRoot00.Width = 64;
                MenuRoot00.Height = 64;
                MenuRoot00.Tag = "MenuRoot00";
                MenuRoot00.HorizontalAlignment = HorizontalAlignment.Left;
                MenuRoot00.MouseUp += NewImage_MouseUp;

                var total = SiteIcons.Children.Count;

                SiteIcons.Children.Insert(0, MenuRoot00);


            }
            catch
            {

            }
        }

        private void IconSelected_MouseUp(object sender, RoutedEventArgs e)
        {
            try
            {
                //uiBrowser.Navigate(configuration.listSites[SiteIcons.Children.Count - 2]);
                //uiBrowser.Visibility = Visibility.Visible;
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

                SiteIcons.Children.Clear();
                //clear the sites and reparse the config


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
