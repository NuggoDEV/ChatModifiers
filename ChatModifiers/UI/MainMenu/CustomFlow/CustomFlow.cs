using BeatSaberMarkupLanguage;
using HMUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatModifiers.UI.MainMenu.CustomFlow
{
    public class CustomFlow : FlowCoordinator
    {
        public ViewController _view;
        public string _title;
        public FlowCoordinator _lastFlowCoordinator;

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            SetLeftScreenViewController(null, ViewController.AnimationType.None);
            SetRightScreenViewController(null, ViewController.AnimationType.None);
            _lastFlowCoordinator.DismissFlowCoordinator(this);
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                SetTitle(_title);
                showBackButton = true;
                ProvideInitialViewControllers(_view);
            }
        }
    }
}
