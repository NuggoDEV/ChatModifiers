using ChatModifiers.API;
using ChatModifiers.UI.ModifiersMenuHijacking;

namespace ChatModifiers.Utilities
{
    public static class StaticUtils
    {
        public static string GetModifierIdentifier(CustomModifier modifier)
        {
            return $"{modifier.Name}_{modifier.Author}";
        }

        internal static string GetModifierIdentifier(ModifierListItem item)
        {
            return $"{item.modifierTitle}_{item.author}";
        }
    }
}
