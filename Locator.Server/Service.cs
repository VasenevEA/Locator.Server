using System.IO;
using Newtonsoft.Json;
using Locator.Server.Models;

namespace Locator.Server
{
    public static class Service
    {
        private static string configName = "config.txt";

        public static Config readConfig()
        {
            string source = File.ReadAllText(configName);
            return JsonConvert.DeserializeObject<Config>(source);
        }
    }
}
