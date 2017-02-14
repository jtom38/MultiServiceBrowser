using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MultiServiceBrowser
{
    public class ListSites
    {
        public string site { get; set; }
        public string url { get; set; }
        public string args { get; set; }
        public string iconPath { get; set; }
    }

    public class configuration
    {

        public static List<ListSites> listSites = new List<ListSites>();

        public static string sitesFile = Directory.GetCurrentDirectory() + "\\config.json";

        /// <summary> Save the current configuration object to a file. </summary>
        /// <param name="loc"> The configuration file's location. </param>
        public void SaveFile()
        {
            string json = JsonConvert.SerializeObject(listSites, Formatting.Indented);

            if (!File.Exists(sitesFile))
                File.Create(sitesFile).Close();

            File.WriteAllText(sitesFile, json);
        }

        public void LoadFile()
        {
            try
            {
                if (File.Exists(sitesFile))
                {
                    string json = File.ReadAllText(sitesFile);

                    listSites = JsonConvert.DeserializeObject<List<ListSites>>(json);
                }
                else
                {
                    SaveFile();
                }
            }
            catch
            {

            }
        }

        /// <summary> Load the information saved in your configuration file. </summary>
        /// <param name="loc"> The configuration file's location. </param>
        public static configuration LoadFileStatic()
        {
            string json = File.ReadAllText(sitesFile);
            return JsonConvert.DeserializeObject<configuration>(json);
        }

    }
}
