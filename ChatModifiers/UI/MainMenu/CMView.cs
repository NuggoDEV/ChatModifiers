using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using TMPro;

namespace ChatModifiers.UI
{
    [ViewDefinition("ChatModifiers.UI.MainMenu.CMView.bsml")]
    internal class CMView : BSMLAutomaticViewController
    {
        [UIComponent("fortnitetext")]
        private TextMeshProUGUI fortnitetext = null;

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {

        }
    }
}
