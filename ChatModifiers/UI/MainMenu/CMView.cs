using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using ChatModifiers.API;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChatModifiers.UI.MainMenu
{
    [HotReload(RelativePathToLayout = @"CMView.bsml")]
    [ViewDefinition("ChatModifiers.UI.MainMenu.CMView.bsml")]
    internal class CMView : BSMLAutomaticViewController
    {
        [UIComponent("modifierList")]
        public CustomCellListTableData modifierList;

        [UIValue("modifiersContents")]
        private List<object> modifiersContents => new List<object>();

        [UIObject("noModsInstalledVertical")]
        private GameObject noModsInstalledVertical;

        [UIAction("reloadModifiers")]
        internal void ReloadModifiers()
        {
            LoadModifiers();
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            LoadModifiers();
        }

        internal void LoadModifiers()
        {
            modifierList.data.Clear();
            if (RegistrationManager._registeredModifiers.Count == 0)
            {
                noModsInstalledVertical.SetActive(true);
                modifierList.gameObject.SetActive(false);
                return;
            }
            else
            {
                noModsInstalledVertical.SetActive(false);
                modifierList.gameObject.SetActive(true);

                int i = 0;
                foreach (var modifier in RegistrationManager._registeredModifiers)
                {
                    modifierList.data.Add(
                        new ModifierListItem(
                            i,
                            modifier.Name,
                            modifier.Author,
                            BeatSaberMarkupLanguage.Utilities.FindSpriteInAssembly(modifier.PathToIcon)
                        )
                    );
                    i++;
                }
                modifierList.data.Cast<object>();
                modifierList.tableView.ReloadData();
            }
        }
    }
}
