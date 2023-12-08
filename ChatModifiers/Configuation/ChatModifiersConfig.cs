using ChatModifiers.API;
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

        internal static Config Instance { get; set; } = new Config();

        public Dictionary<string, ModifierSettings> Mods { get; set; } = new Dictionary<string, ModifierSettings>();

        public void Save()
        {
            string json = JsonConvert.SerializeObject(Instance, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(FileName, json);
        }

        public void Load()
        {
            if (System.IO.File.Exists(FileName))
            {
                string json = System.IO.File.ReadAllText(FileName);
                Instance = JsonConvert.DeserializeObject<Config>(json);
            }
            else
            {
                Save();
            }
        }
    }

    public class ModifierSettings
    {
        public bool Enabled { get; set; } = true;
        public Dictionary<string, object> AdditionalSettings { get; set; } = new Dictionary<string, object>();
        public ModifierSettings(Dictionary<string, object> additionalSettings) { AdditionalSettings = additionalSettings; }
    }
}
