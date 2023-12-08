using IPA.Config.Stores;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ChatModifiers
{
    internal class Config
    {
        internal const string FileName = "./UserData/ChatModifiersConfig.json";

        internal static Config Instance { get; set; }

        public Dictionary<string, ModifierSettings> Mods { get; set; }

        public Config() => Mods = new Dictionary<string, ModifierSettings>();

        public void Save() => File.WriteAllText(FileName, JsonConvert.SerializeObject(Instance, Formatting.Indented));

        public void Load()
        {
            if (File.Exists(FileName))
                Instance = JsonConvert.DeserializeObject<Config>(File.ReadAllText(FileName));
            else
                Save();
        }
    }

    public class ModifierSettings
    {
        public bool Enabled { get; set; }
        public Dictionary<string, object> AdditionalSettings { get; set; }
        public ModifierSettings(Dictionary<string, object> additionalSettings) { AdditionalSettings = additionalSettings; }
    }
}
