using HMUI;
using System;
using System.Collections.Generic;

namespace ChatModifiers.API
{
    /// <summary>
    /// Represents a custom chat modifier with configurable settings.
    /// </summary>
    public class CustomModifier
    {
        /// <summary>
        /// Gets or sets the name of the custom modifier.
        /// </summary>
        public string Name { get; set; } = "Default";

        /// <summary>
        /// Gets or sets a short description of the custom modifier.
        /// </summary>
        public string Description { get; set; } = "Default Description";

        /// <summary>
        /// Gets or sets the author of the custom modifier.
        /// </summary>
        public string Author { get; set; } = "Default Author";

        /// <summary>
        /// Gets or sets the path to the icon for the custom modifier.
        /// </summary>
        public string PathToIcon { get; set; } = "Default Path";

        /// <summary>
        /// Gets or sets the command keyword in chat that triggers the custom modifier.
        /// </summary>
        public string CommandKeyword { get; set; } = "default";

        /// <summary>
        /// Gets or sets the function to be executed when the custom modifier is triggered.
        /// </summary>
        public Action<MessageInfo, object[]> Function { get; set; } = null;

        /// <summary>
        /// Gets or sets an array of ArgumentInfo objects representing the arguments for the custom modifier's function.
        /// </summary>
        public ArgumentInfo[] Arguments { get; set; } = null;

        /// <summary>
        /// Gets or sets the areas in which the custom modifier is active.
        /// </summary>
        public Areas ActiveAreas { get; set; } = Areas.None;

        /// <summary>
        /// Gets or sets a dictionary to hold settings for the custom modifier.
        /// </summary>
        public Dictionary<string, object> Settings { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets the ViewController used for the custom modifier's settings UI.
        /// </summary>
        public ViewController SettingsViewController { get; set; } = null;

        /// <summary>
        /// Gets the ModifierSettings for the custom modifier.
        /// </summary>
        public ModifierSettings ModifierSettings { get => ModifierSettings.GetModifierSettingsFromIdentifier(Utilities.StaticUtils.GetModifierIdentifier(this)); }

        /// <summary>
        /// Registers the custom modifier to the game using RegistrationManager.RegisterModifier(this).
        /// </summary>
        /// <returns>True if registration is successful, false otherwise.</returns>
        public bool Register()
        {
            return RegistrationManager.RegisterModifier(this);
        }

        /// <summary>
        /// Unregisters the custom modifier from the game using RegistrationManager.UnregisterModifier(this).
        /// </summary>
        /// <returns>True if unregistration is successful, false otherwise.</returns>
        public bool Unregister()
        {
            return RegistrationManager.UnregisterModifier(this);
        }

        /// <summary>
        /// Saves the settings of the custom modifier using Config.Instance.Save().
        /// </summary>
        public void SaveSettings()
        {
            Config.Instance.Save();
        }

        /// <summary>
        /// Creates a new instance of CustomModifier.
        /// </summary>
        /// <param name="name">The name of the custom modifier.</param>
        /// <param name="description">A short description of what the custom modifier does.</param>
        /// <param name="author">The author of the custom modifier.</param>
        /// <param name="pathToIcon">The name of the embedded resource in your assembly.</param>
        /// <param name="commandKeyword">The keyword sent in chat that will execute the custom modifier's function.</param>
        /// <param name="function">The function to be executed, must have parameters -> (MessageInfo messageInfo, object[] args).</param>
        /// <param name="arguments">An array of ArgumentInfo objects, each containing the name of the argument and its Type.</param>
        /// <param name="areas">Which areas in the game the custom modifier should be active for.</param>
        /// <param name="settings">A string, object dictionary to hold settings for the custom modifier.</param>
        /// <param name="viewController">View Controller to use for the custom modifier's settings UI.</param>
        public CustomModifier(string name, string description, string author, string pathToIcon, string commandKeyword, Action<MessageInfo, object[]> function, ArgumentInfo[] arguments, Areas areas, Dictionary<string, object> settings, ViewController viewController = null)
        {
            Name = name;
            Description = description;
            Author = author;
            PathToIcon = pathToIcon;
            CommandKeyword = commandKeyword;
            Function = function;
            Arguments = arguments;
            ActiveAreas = areas;
            Settings = settings;
            SettingsViewController = viewController;
        }
    }
}
