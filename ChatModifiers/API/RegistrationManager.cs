using ChatModifiers.UI.ModifiersMenuHijacking;
using System;
using System.Collections.Generic;

namespace ChatModifiers.API
{
    /// <summary>
    /// Manages the registration and unregistration of custom modifiers.
    /// </summary>
    public class RegistrationManager
    {
        /// <summary>
        /// Internal list of registered custom modifiers.
        /// </summary>
        internal static List<CustomModifier> _registeredModifiers = new List<CustomModifier>();

        /// <summary>
        /// Registers a custom modifier to the game.
        /// </summary>
        /// <param name="modifier">The custom modifier to register.</param>
        /// <returns>True if registration is successful, false otherwise.</returns>
        public static bool RegisterModifier(CustomModifier modifier)
        {
            try
            {
                if (_registeredModifiers.Contains(modifier))
                {
                    Plugin.Log.Info($"Modifier {modifier.Name} is already registered.");
                    return false;
                }

                var settings = new ModifierSettings(modifier.Settings);
                _registeredModifiers.Add(modifier);

                if (!Config.Instance.Modifiers.ContainsKey(Utilities.StaticUtils.GetModifierIdentifier(modifier)))
                {
                    settings.Enabled = false;
                    Config.Instance.Modifiers.Add(Utilities.StaticUtils.GetModifierIdentifier(modifier), settings);
                    Config.Instance.Save();
                }

                CustomModifierMenuUI.shouldRefresh = true;
                Plugin.Log.Info($"Registered Modifier: {modifier.Name}");
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.Error($"Error during registering modifier {modifier.Name}: {ex.Message}");
                return false;
            }
            finally
            {
                Plugin.Log.Notice($"Registration {(RegistrationManager._registeredModifiers.Contains(modifier) ? "successful" : "failed")} for modifier {modifier.Name}");
            }
        }

        /// <summary>
        /// Unregisters a custom modifier from the game.
        /// </summary>
        /// <param name="modifier">The custom modifier to unregister.</param>
        /// <returns>True if unregistration is successful, false otherwise.</returns>
        public static bool UnregisterModifier(CustomModifier modifier)
        {
            try
            {
                if (!_registeredModifiers.Contains(modifier))
                {
                    Plugin.Log.Info($"Modifier {modifier.Name} is not registered.");
                    return false;
                }

                _registeredModifiers.Remove(modifier);
                CustomModifierMenuUI.shouldRefresh = true;
                Plugin.Log.Info($"Unregistered Modifier: {modifier.Name}");
                return true;
            }
            catch (Exception ex)
            {
                Plugin.Log.Error($"Error during unregistering modifier {modifier.Name}: {ex.Message}");
                return false;
            }
            finally
            {
                Plugin.Log.Notice($"Unregistration {(RegistrationManager._registeredModifiers.Contains(modifier) ? "failed" : "successful")} for modifier {modifier.Name}");
            }
        }

        /// <summary>
        /// Logs information about all registered modifiers.
        /// </summary>
        /// <param name="allDetails">If true, logs detailed information about each modifier.</param>
        internal static void LogAllModifiers(bool allDetails)
        {
            Plugin.Log.Notice("Logging all modifiers");
            foreach (CustomModifier modifier in _registeredModifiers)
            {
                if (allDetails)
                {
                    Plugin.Log.Info($"Modifier: {modifier.Name} | Author: {modifier.Author} | Keyword: {modifier.CommandKeyword} | Description: {modifier.Description} | Arguments: {modifier.Arguments} | Settings: {modifier.Settings.ToString()}");
                }
                else
                {
                    Plugin.Log.Info($"Modifier: {modifier.Name} | Author: {modifier.Author}");
                }
            }
        }
    }
}
