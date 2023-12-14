using IPA.Config.Stores;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ChatModifiers
{
    public class Config
    {
        internal const string FileName = "./UserData/ChatModifiersConfig.json";

        public static Config Instance { get; set; }

        public Dictionary<string, ModifierSettings> Modifiers { get; set; }

        public Config() => Modifiers = new Dictionary<string, ModifierSettings>();

        public void Save() => File.WriteAllText(FileName, JsonConvert.SerializeObject(Instance, Formatting.Indented));

        public void Load()
        {
            if (File.Exists(FileName))
                Instance = JsonConvert.DeserializeObject<Config>(File.ReadAllText(FileName));
            else
                Save();
        }
    }

    /// <summary>
    /// Represents settings for a custom modifier.
    /// </summary>
    public class ModifierSettings
    {
        /// <summary>
        /// Gets or sets the identifier for the modifier settings.
        /// </summary>
        internal string _identifier;

        /// <summary>
        /// Gets or sets whether the modifier is enabled.
        /// </summary>
        internal bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets additional settings for the modifier.
        /// </summary>
        public Dictionary<string, object> AdditionalSettings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierSettings"/> class.
        /// </summary>
        /// <param name="additionalSettings">Additional settings for the modifier.</param>
        public ModifierSettings(Dictionary<string, object> additionalSettings)
        {
            AdditionalSettings = additionalSettings;
        }

        /// <summary>
        /// Gets the modifier settings associated with the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the modifier settings.</param>
        /// <returns>The modifier settings or null if not found.</returns>
        public static ModifierSettings GetModifierSettingsFromIdentifier(string identifier)
        {
            if (Config.Instance.Modifiers.ContainsKey(identifier))
                return Config.Instance.Modifiers[identifier];
            else
                return null;
        }
    }

}
