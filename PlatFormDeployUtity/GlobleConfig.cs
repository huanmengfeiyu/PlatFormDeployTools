using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PlatFormDeployUtility
{
    public class GlobleConfig
    {
        private static string lastDirectory;
        public static string LastDirectory
        {
            get { return lastDirectory = ConfigurationManager.AppSettings["LastDirectory"]; }
            set
            {
                lastDirectory = value;
                SaveConfig("LastDirectory", lastDirectory);
            }
        }
        private static void SaveConfig(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
