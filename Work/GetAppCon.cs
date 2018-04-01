using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Work
{
    class GetAppCon
    {
        public Dictionary<string, string> ReadAllSettings()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                foreach (var key in appSettings.AllKeys)
                {
                    dic.Add(key, appSettings[key]);
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }

            return dic;
        }

        public string ReadSetting(string key)
        {
            string re = "";

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                re = appSettings[key] ?? "Not Found";
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }

            return re;
        }

        public void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;

                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Error writing app settings");
            }
        }
    }
}
