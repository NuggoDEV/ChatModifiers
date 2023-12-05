using System.Collections.Generic;

namespace ChatModifiers.API
{
    public class RegistrationManager
    {
        internal static List<CustomModifier> _registeredModifiers = new List<CustomModifier>();

        public static void RegisterModifier(CustomModifier modifier)
        {
            _registeredModifiers.Add(modifier);
            Plugin.Log.Info($"Registered Modifier: {modifier.Name}");
        }
        public static void UnregisterModifier(CustomModifier modifier)
        {
            _registeredModifiers.Remove(modifier);
            Plugin.Log.Info($"Unregistered Modifier: {modifier.Name}");
        }
    }
}
