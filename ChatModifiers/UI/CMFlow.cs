using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HMUI;
using SiraUtil.Logging;
using Zenject;

namespace ChatModifiers.UI
{
    internal class CMFlow : FlowCoordinator
    {
        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (!firstActivation)
                return;
            Plugin.Log.Info("Setting up FlowCoordinator");

            SetTitle("Chat Modifiers");
            showBackButton = true;
            ProvideInitialViewControllers(BeatSaberUI.CreateViewController<CMView>());
        }

        protected override void BackButtonWasPressed(ViewController topViewController) => BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);

        private void ShowFlow()
        {
            FlowCoordinator parentFlow = BeatSaberUI.MainFlowCoordinator.YoungestChildFlowCoordinatorOrSelf();
            BeatSaberUI.PresentFlowCoordinator(parentFlow, this);
        }

        internal static void CreateMenuButton()
        {
            MenuButton menuButton = new MenuButton("Chat Modifiers", "Configure Chat Modifiers", () => BeatSaberUI.CreateFlowCoordinator<CMFlow>().ShowFlow());
            MenuButtons.instance.RegisterButton(menuButton);
        }
    }
}
