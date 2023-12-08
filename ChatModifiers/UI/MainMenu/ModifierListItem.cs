using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using ChatModifiers.API;
using ChatModifiers.Utilities;
using HMUI;
using System;
using System.Linq;
using UnityEngine;

namespace ChatModifiers.UI.MainMenu
{
    internal class ModifierListItem
    {
        public int index;

        [UIValue("modifierTitle")] public string modifierTitle;
        [UIValue("modifierAuthor")] public string modifierAuthor;

        [UIComponent("modifierImage")] public ImageView modifierImage;
        private Sprite modifierImageSprite;

        public ModifierListItem(int index, string modifierTitle, string modifierAuthor, Sprite modifierImageSprite)
        {
            this.index = index;
            this.modifierTitle = modifierTitle;
            this.modifierAuthor = modifierAuthor;
            this.modifierImageSprite = modifierImageSprite;
        }

        [UIAction("#post-parse")]
        public void Setup()
        {
            modifierImage.sprite = modifierImageSprite;
            this.modifierImage.material = Resources.FindObjectsOfTypeAll<Material>().First(x => x.name == "UINoGlowRoundEdge");
        }

        [UIAction("modifierClicked")]
        public void modifierPlaylistClicked()
        {
            CustomModifier clickedModifier = RegistrationManager._registeredModifiers[index];
            Plugin.Log.Info("Opening modifier menu for " + StaticUtils.GetModifierIdentifier(clickedModifier));
            ShowModifierFlowCoordinator(clickedModifier);
        }

        internal void ShowModifierFlowCoordinator(CustomModifier customModifier)
        {
            FlowCoordinator flowCoordinator = BeatSaberUI.CreateFlowCoordinator<CustomFlow.CustomFlow>();
            CustomFlow.CustomFlow customFlow = flowCoordinator as CustomFlow.CustomFlow;

            if (customFlow != null)
            {
                customFlow._title = customModifier.Name;
                customFlow._view = customModifier.SettingsViewController;
                var activeFlow = DeepestChildFlowCoordinator(BeatSaberUI.MainFlowCoordinator);
                customFlow._lastFlowCoordinator = activeFlow;
                activeFlow.PresentFlowCoordinator(flowCoordinator);
            }
            else
            {
                Plugin.Log.Error("Unable to find flow coordinator! Cannot show Custom Flow Coordinator.");
            }
        }

        internal static FlowCoordinator DeepestChildFlowCoordinator(FlowCoordinator root)
        {
            var flow = root.childFlowCoordinator;
            if (flow == null) return root;
            if (flow.childFlowCoordinator == null || flow.childFlowCoordinator == flow)
            {
                return flow;
            }
            return DeepestChildFlowCoordinator(flow);
        }
    }
}
