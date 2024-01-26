using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aspose.OCR.Cloud.CommandLineTool
{
    public class Config
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }


        public Config(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;

        }


        public static Config GetConfig()
        {
            try
            {
                string envData = Environment.GetEnvironmentVariable("AsposeOCRCloudCLIConfig", EnvironmentVariableTarget.User);
                if (envData == null) { Console.WriteLine("Warning! Config not set"); };
                string decodedjson = Encoding.UTF8.GetString(Convert.FromBase64String(envData));
                Config? config = JsonSerializer.Deserialize<Config>(decodedjson);
                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load configuration: {ex}");
                throw;
            }
        }


        public static void SaveConfig(Config config)
        {
            try
            {

                string json = JsonSerializer.Serialize(config);
                string encodedconfig = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
                Environment.SetEnvironmentVariable("AsposeOCRCloudCLIConfig", encodedconfig, EnvironmentVariableTarget.User);
                Console.WriteLine("Configuration successfully saved");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Failed to save configuration: {ex}");
            }
        }


        public static void Configure(string[] args)
        {
            string clinetId = args[Array.IndexOf(args, "-clientid") + 1];
            string clinetSecret = args[Array.IndexOf(args, "-secret") + 1];
            Config config = new Config(clinetId, clinetSecret);
            Config.SaveConfig(config);
        }
    }


    public enum Actions
    {
        Undefined = 0,
        Configure = 1,
        RecognizeImage = 2,
        GetHelp = 3,
    }
}
