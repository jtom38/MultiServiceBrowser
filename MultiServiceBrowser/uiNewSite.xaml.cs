using MahApps.Metro.Controls;
using System.Windows;
using System;

namespace MultiServiceBrowser
{
    /// <summary>
    /// Interaction logic for uiNewSite.xaml
    /// </summary>
    public partial class uiNewSite : MetroWindow
    {
        public uiNewSite()
        {
            InitializeComponent();
        }

        private void eleButtonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //check all the data in the fields
                string name = eleName.Text; //cant be null
                string url = eleURL.Text; //cant be null
                string args = eleArgs.Text; //this can be null
                string icon = eleIconPath.Text;  // if null try to pull the favicon

                configuration _config = new configuration();
                configuration.listSites.Add(new ListSites
                {
                    site = eleName.Text,
                    url = eleURL.Text,
                    args = eleArgs.Text
                });
                _config.SaveFile();
                

            }
            catch
            {

            }
        }


    }
}
