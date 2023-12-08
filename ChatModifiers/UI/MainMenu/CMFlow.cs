using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using ChatModifiers.UI.MainMenu;
using HMUI;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ChatModifiers.UI
{
    internal class CMFlow : FlowCoordinator, IInitializable
    {
        [Inject] internal CMView _view;

        private FlowCoordinator _lastFlowCoordinator;

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                SetTitle("ChatModifiers");
                showBackButton = true;
                ProvideInitialViewControllers(_view);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            SetLeftScreenViewController(null, ViewController.AnimationType.None);
            SetRightScreenViewController(null, ViewController.AnimationType.None);
            _lastFlowCoordinator.DismissFlowCoordinator(this);
        }

        internal void ShowMainFlowCoordinator()
        {
            CMFlow flowCoordinator = Resources.FindObjectsOfTypeAll<CMFlow>().FirstOrDefault();
            if (flowCoordinator != null)
            {
                var activeFlow = DeepestChildFlowCoordinator(BeatSaberUI.MainFlowCoordinator);
                activeFlow.PresentFlowCoordinator(flowCoordinator);
                flowCoordinator._lastFlowCoordinator = activeFlow;
            }
            else
            {
                Plugin.Log.Error("Unable to find flow coordinator! Cannot show ChatModifiers Flow Coordinator.");
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

        public void Initialize()
        {
            MenuButton menuButton = new MenuButton("Chat Modifiers", "Configure Chat Modifiers", () => this.ShowMainFlowCoordinator());
            MenuButtons.instance.RegisterButton(menuButton);
        }
    }
}