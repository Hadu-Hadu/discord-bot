using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TheGentleman2._0
{
    class BotConfig
    {
        private const string configFolder = "config";
        private const string configFile = "config.json";
        public static BotConfigSettings Config;

        static BotConfig()
        {
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }
            if (!File.Exists(configFolder + "/" + configFile))
            {
                Config = new BotConfigSettings();
                string json = JsonConvert.SerializeObject(Config, Formatting.Indented);
                File.WriteAllText(configFolder + "/" + configFile, json);
            }
            else
            {
                string json = File.ReadAllText(configFolder + "/" + configFile);
                Config = JsonConvert.DeserializeObject<BotConfigSettings>(json);
            }
        }
    }
    
    public struct BotConfigSettings
    {
        public string BotToken { get; set; }
        public string BotPrefix { get; set; }

    }
}
