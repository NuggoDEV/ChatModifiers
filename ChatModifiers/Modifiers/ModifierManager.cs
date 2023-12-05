using ChatModifiers.API;

namespace ChatModifiers.Modifiers
{
    internal class ModifierManager
    {
        public static void RegisterPluginModifiers()
        {
            RegistrationManager.RegisterModifier(WorldRotation.modifier);
        }
    }
}
