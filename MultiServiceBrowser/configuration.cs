using Newtonsoft.Json;
using System.IO;

namespace MultiServiceBrowser
{
    public class configuration
    {
        public static string configFile = Directory.GetCurrentDirectory() + "\\config.json";

        /// <summary>
        /// String format is { Name, url, args(username/password), icon file, position}
        /// </summary>
        public string[] Site00 { get; set; }
        public string[] Site01 { get; set; }
        public string[] Site02 { get; set; }
        public string[] Site03 { get; set; }
        public string[] Site04 { get; set; }
        public string[] Site05 { get; set; }
        public string[] Site06 { get; set; }
        public string[] Site07 { get; set; }
        public string[] Site08 { get; set; }
        public string[] Site09 { get; set; }

        public configuration()
        {
            Site00 = new string[] { };
            Site01 = new string[] { };
            Site02 = new string[] { };
            Site03 = new string[] { };
            Site04 = new string[] { };
            Site05 = new string[] { };
            Site06 = new string[] { };
            Site07 = new string[] { };
            Site08 = new string[] { };
            Site09 = new string[] { };
        }

        /// <summary> Save the current configuration object to a file. </summary>
        /// <param name="loc"> The configuration file's location. </param>
        public void SaveFile()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);

            if (!File.Exists(configFile))
                File.Create(configFile).Close();

            File.WriteAllText(configFile, json);
        }

        /// <summary> Load the information saved in your configuration file. </summary>
        /// <param name="loc"> The configuration file's location. </param>
        public static configuration LoadFile()
        {
            string json = File.ReadAllText(configFile);
            return JsonConvert.DeserializeObject<configuration>(json);
        }
    }
}
