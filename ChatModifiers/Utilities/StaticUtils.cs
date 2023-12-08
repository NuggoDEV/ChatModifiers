using ChatModifiers.API;
using ChatModifiers.UI.ModifiersMenuHijacking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatModifiers.Utilities
{
    internal static class StaticUtils
    {
        internal static string GetModifierIdentifier(CustomModifier modifier)
        {
            return $"{modifier.Name}_{modifier.Author}";
        }

        internal static string GetModifierIdentifier(ModifierListItem item)
        {
            return $"{item.modifierTitle}_{item.author}";
        }
    }
}
