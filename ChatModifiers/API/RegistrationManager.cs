using System;
using System.Collections.Generic;

namespace ChatModifiers.API
{
    public class RegistrationManager
    {
        internal static List<CustomModifier> _registeredModifiers = new List<CustomModifier>();

        public static bool RegisterModifier(CustomModifier modifier)
        {
            try
            {
                if (_registeredModifiers.Contains(modifier))
                {
                    Plugin.Log.Info($"Modifier {modifier.Name} is already registered.");
                    return false;
                }

                _registeredModifiers.Add(modifier);
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
                Plugin.Log.Info($"Registration {(RegistrationManager._registeredModifiers.Contains(modifier) ? "successful" : "failed")} for modifier {modifier.Name}");
            }
        }

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
                Plugin.Log.Info($"Unregistration {(RegistrationManager._registeredModifiers.Contains(modifier) ? "failed" : "successful")} for modifier {modifier.Name}");
            }
        }
    }
}
