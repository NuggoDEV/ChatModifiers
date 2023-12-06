using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using ChatModifiers.API;
using HMUI;
using IPA.Config.Data;
using SiraUtil.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace ChatModifiers.UI.ModifiersMenuHijacking
{
    internal class CustomModifierMenuUI : IInitializable, IFixedTickable
    {
        [Inject] internal SiraLog _log;
        internal static bool shouldRefresh = false;
        internal GameplayModifiersPanelController gameplayModifiersPanelController;
        private Dictionary<CustomModifier, GameplayModifierToggle> modifierButtonMap = new Dictionary<CustomModifier, GameplayModifierToggle>();

        [UIObject("theContainer")]
        private GameObject theContainer;

        internal GameObject hintText;
        internal GameObject modifiersTable;
        internal GameObject statusText;

        [UIComponent("modifiersToggleSetting")]
        internal ToggleSetting modifiersToggleSetting;

        [UIComponent("modifierGrid")]
        private GridLayoutGroup modifierGrid;

        [UIObject("modifierContainer")]
        private GameObject modifierContainer;

        [UIObject("noModsInstalledText")]
        private GameObject noModsInstalledText;


        // yoinked from bsml and altered to work

        GameplayModifierToggle toggleTemplate;
        public GameplayModifierToggle CreateModifier(Transform parent, CustomModifier customModifier)
        {
            if (modifierButtonMap.TryGetValue(customModifier, out var existingButton))
            {
                existingButton.transform.SetParent(parent, false);
                return existingButton;
            }
            if (toggleTemplate == null)
            {
                toggleTemplate = Resources.FindObjectsOfTypeAll<GameplayModifierToggle>().First((GameplayModifierToggle x) => x.name == "InstaFail");
            }

            GameplayModifierToggle gameplayModifierToggle = Object.Instantiate(toggleTemplate, parent, worldPositionStays: false);
            gameplayModifierToggle.name = "BSMLModifier";
            GameObject gameObject = gameplayModifierToggle.gameObject;
            gameObject.SetActive(value: false);
            Object.Destroy(gameplayModifierToggle);
            Object.Destroy(gameObject.GetComponent<HoverTextSetter>());
            Object.Destroy(gameObject.transform.Find("Multiplier").gameObject);
            GameObject gameObject2 = gameObject.transform.Find("Name").gameObject;
            TextMeshProUGUI component = gameObject2.GetComponent<TextMeshProUGUI>();
            component.text = customModifier.Name;
            List<Component> components = gameObject.AddComponent<ExternalComponents>().components;
            components.Add(component);
            ImageView imageView = gameObject.transform.Find("Icon").GetComponent<Image>() as ImageView;
            imageView.sprite = BeatSaberMarkupLanguage.Utilities.FindSpriteInAssembly(customModifier.PathToIcon);
            components.Add(gameObject.transform.Find("Icon").GetComponent<Image>());
            ToggleSetting toggleSetting = gameObject.AddComponent<ToggleSetting>();
            toggleSetting.toggle = gameObject.GetComponent<Toggle>();
            toggleSetting.toggle.onValueChanged.RemoveAllListeners();
            gameplayModifierToggle.transform.SetParent(parent, false);
            if(Config.instance.enabledModifiers.Contains(GetModifierIdentifier(customModifier)))
            {
                toggleSetting.toggle.isOn = true;
                toggleSetting.Value = true;
                toggleSetting.ApplyValue();
            }
            else
            {
                toggleSetting.toggle.isOn = false;
                toggleSetting.Value = false;
                toggleSetting.ApplyValue();
            }
            toggleSetting.toggle.onValueChanged.AddListener((bool value) =>
            {
                toggleSetting.Value = value;
                toggleSetting.ApplyValue();
                UpdateConfig(customModifier, !value);
            });
            modifierButtonMap.Add(customModifier, gameplayModifierToggle);
            gameObject.SetActive(value: true);
            return gameplayModifierToggle;
        }

        internal bool UpdateConfig(CustomModifier modifier, bool remove)
        {
            _log.Info(remove ? $"Removing modifier {modifier.Name}" : $"Adding modifier {modifier.Name}");
            try
            {
                if (remove)
                {
                    Config.instance.enabledModifiers.Remove(GetModifierIdentifier(modifier));
                    Config.instance.Save();
                    return true;
                }
                else
                {
                    Config.instance.enabledModifiers.Add(GetModifierIdentifier(modifier));
                    Config.instance.Save();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Error during updating config for modifier {modifier.Name}: {ex.Message}");
                return false;
            }
        }


        [UIAction("toggleChatModifiers")]
        private void ToggleChatModifiers(bool value)
        {
            if (modifiersTable != null)
            {
                modifiersTable.gameObject.SetActive(!value);
            }

            if (hintText != null)
            {
                hintText.gameObject.SetActive(!value);
            }

            if (statusText != null)
            {
                statusText.gameObject.SetActive(!value);
            }

            if (theContainer != null)
            {
                theContainer.gameObject.SetActive(value);
            }
        }


        [UIAction("#post-parse")]
        private void PostParse()
        {
            if (theContainer == null) return;
        }

        private string GetModifierIdentifier(CustomModifier modifier)
        {
            return $"{modifier.Name}_{modifier.Author}";
        }

        private string GetModifierIdentifier(ModifierListItem item)
        {
            return $"{item.modifierTitle}_{item.author}";
        }

        private void ReloadModifiers()
        {
            _log.Notice("Reloading modifiers");

            if (Plugin.debug) RegistrationManager.LogAllModifiers(true);

            ClearModifierGrid();

            List<CustomModifier> registeredModifiers = RegistrationManager._registeredModifiers;

            if (registeredModifiers.Count == 0)
            {
                noModsInstalledText.gameObject.SetActive(true);
                modifierContainer.gameObject.SetActive(false);
                return;
            }
            else
            {
                noModsInstalledText.gameObject.SetActive(false);
                modifierContainer.gameObject.SetActive(true);
            }

            foreach (CustomModifier modifier in registeredModifiers)
            {
                if (!modifierButtonMap.ContainsKey(modifier))
                {
                    CreateModifier(modifierGrid.transform, modifier);
                }
            }
        }

        private void ClearModifierGrid()
        {
            //_log.Info($"Attempting to clear modifierGrid with {modifierGrid.transform.childCount} children if needed.");

            foreach (Transform child in modifierGrid.transform)
            {
                if (modifierButtonMap.ContainsValue(child.GetComponent<GameplayModifierToggle>()))
                {
                    var customModifier = modifierButtonMap.FirstOrDefault(x => x.Value == child.GetComponent<GameplayModifierToggle>()).Key;

                    if (customModifier != null)
                    {
                        modifierButtonMap.Remove(customModifier);
                    }

                    Object.DestroyImmediate(child.gameObject);
                }
            }

            //_log.Info("ModifierGrid cleared.");
        }

        private void SetupBSML(GameObject parent, string m)
        {
            BeatSaberMarkupLanguage.BSMLParser.instance.Parse(BeatSaberMarkupLanguage.Utilities.GetResourceContent(System.Reflection.Assembly.GetExecutingAssembly(), m), parent, this);
            theContainer.gameObject.SetActive(false);
        }

        private bool isInitialized = false;
        public void Initialize()
        {
            gameplayModifiersPanelController = Resources.FindObjectsOfTypeAll<GameplayModifiersPanelController>().FirstOrDefault();

            SetupBSML(gameplayModifiersPanelController.gameObject, "ChatModifiers.UI.ModifiersMenuHijacking.view.bsml");
            hintText = gameplayModifiersPanelController.transform.Find("HintText")?.gameObject;
            modifiersTable = gameplayModifiersPanelController.transform.Find("Modifiers")?.gameObject;
            statusText = gameplayModifiersPanelController.transform.Find("Info")?.gameObject;
            MoveAllGOsUp(hintText, modifiersTable, statusText);
            Resources.FindObjectsOfTypeAll<SelectModifiersViewController>().FirstOrDefault().didActivateEvent += CustomModifierMenuUI_didActivateEvent;
            isInitialized = true;
        }

        private void MoveAllGOsUp(GameObject a, GameObject b, GameObject c)
        {
            RectTransform rectTransformA = a.GetComponent<RectTransform>();
            RectTransform rectTransformB = b.GetComponent<RectTransform>();
            RectTransform rectTransformC = c.GetComponent<RectTransform>();

            if (rectTransformA != null)
            {
                rectTransformA.anchoredPosition += new Vector2(0f, 4.25f);
            }

            if (rectTransformB != null)
            {
                rectTransformB.anchoredPosition += new Vector2(0f, 5f);
            }

            if (rectTransformC != null)
            {
                rectTransformC.anchoredPosition += new Vector2(0f, 3.75f);
            }
        }


        private void CustomModifierMenuUI_didActivateEvent(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            //_log.Info("Activating");
            ReloadModifiers();
        }

        public void FixedTick()
        {
            if (shouldRefresh &&  isInitialized)
            {
                ReloadModifiers();
                shouldRefresh = false;
            }
        }
    }

    internal class ModifierListItem
    {
        public int index;


        [UIComponent("modifierTitle")] public TextMeshProUGUI modifierTitle;

        [UIComponent("authorText")] public TextMeshProUGUI author;

        public string description;

        [UIComponent("thumbnailImage")]
        public ImageView thumbnailImage;

        private Sprite modifierImageSprite;

        [UIComponent("backgroundImage")]
        internal ImageView backgroundImage;

        private CustomModifier customModifier;

        public ModifierListItem(int index, CustomModifier customModifier)
        {
            if (customModifier == null) return;
            Plugin.Log.Info($"Creating modifier list item for {customModifier.Name}");
            Plugin.Log.Info(customModifier.Author.ToString());
            Plugin.Log.Info(customModifier.Description.ToString());
            Plugin.Log.Info(customModifier.PathToIcon.ToString());
            Plugin.Log.Info(customModifier.CommandKeyword.ToString());
            this.index = index;
            this.description = customModifier.Description;
            this.modifierImageSprite = BeatSaberMarkupLanguage.Utilities.FindSpriteInAssembly(customModifier.PathToIcon);
            this.customModifier = customModifier;
        }

        [UIAction("#post-parse")]
        public void Setup()
        {
            this.modifierTitle.text = this.modifierTitle.text;
            this.author.text = this.author.text;
            thumbnailImage.sprite = modifierImageSprite;
            if (cool == null)
            {
                cool = Resources.FindObjectsOfTypeAll<Material>().First(x => x.name == "UINoGlowRoundEdge");
            }
            this.thumbnailImage.material = cool;
        }

        private Material cool;
    }

}
