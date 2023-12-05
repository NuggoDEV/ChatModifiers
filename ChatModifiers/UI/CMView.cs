using BeatSaberMarkupLanguage.ViewControllers;

namespace ChatModifiers.UI
{
    internal class CMView : BSMLAutomaticViewController
    {
        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (!firstActivation)
                return;
        }
    }
}
