using MultiServiceBrowser.src;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for PageNewSite.xaml
    /// </summary>
    public partial class PageNewSite : Page
    {
        public PageNewSite()
        {
            InitializeComponent();
        }

        private void eleButtonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //bool saveValue = false;
              
                if(eleURL.Text.Contains("http://") || eleURL.Text.Contains("https://"))
                {
                    //check all the data in the fields
                    string name = eleName.Text; //cant be null
                    string url = eleURL.Text; //cant be null
                    string args = eleArgs.Text; //this can be null
                    string icon = eleIconPath.Text;  // if null try to pull the favicon

                    string hostname = findHostName(eleURL.Text);

                    configuration _config = new configuration();
                    configuration.listSites.Add(new ListSites
                    {
                        site = eleName.Text,
                        host = hostname,
                        url = eleURL.Text,
                        args = eleArgs.Text,
                        iconPath = eleIconPath.Text
                    });

                    _config.SaveFile();

                    eleName.Clear();
                    eleURL.Clear();
                    eleArgs.Clear();
                    eleIconPath.Clear();
                }


                //MainWindow.
            }
            catch
            {

            }
        }

        private string findHostName(string url)
        {
            try
            {
                char[] delimCharacter = { ':', ' ', '/' };

                string[] words = url.Split(delimCharacter);

                return words[3];
            }
            catch
            {
                return null;
            }
        }
    }
}
