using IPA.Config.Stores;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ChatModifiers
{
    internal class Config
    {
        internal const string FileName = "./UserData/ChatModifiersConfig.json";

        internal static Config instance { get; set; }

        public List<string> enabledModifiers { get; set; } = new List<string>();

        public void Save()
        {
            CheckForConfig();
            string json = JsonConvert.SerializeObject(instance, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(FileName, json);
        }

        public void Load()
        {
            if (System.IO.File.Exists(FileName))
            {
                string json = System.IO.File.ReadAllText(FileName);
                instance = JsonConvert.DeserializeObject<Config>(json);
            }
            else
            {
                instance = new Config();
                Save();
            }
        }

        public void CheckForConfig()
        {
            if (!System.IO.File.Exists(FileName))
            {
                System.IO.File.Create(FileName);
            }
        }
    }
}
