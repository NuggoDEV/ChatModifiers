using ChatModifiers.API;
using HMUI;
using IPA.Utilities;
using SiraUtil.Affinity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polyglot;

namespace ChatModifiers.Utilities
{
    internal class WarningHandler : IAffinity
    {
        private void ShowWarningText(SinglePlayerLevelSelectionFlowCoordinator __instance, Action beforeSceneSwitchCallback, bool practice, string titleText, string messageText)
        {
            SimpleDialogPromptViewController viewCon = ReflectionUtil.GetField<SimpleDialogPromptViewController, SinglePlayerLevelSelectionFlowCoordinator>(__instance, "_simpleDialogPromptViewController");

            viewCon.Init(titleText, messageText, Localization.Get("BUTTON_YES"), Localization.Get("BUTTON_NO"), (Action<int>)(buttonNumber =>
            {
                if (buttonNumber == 0)
                {
                    __instance.StartLevel(() =>
                    {
                        ReflectionUtil.InvokeMethod<object, SinglePlayerLevelSelectionFlowCoordinator>(__instance, "DismissViewController", new object[4]
                        {
                    (object) viewCon,
                    (object) ViewController.AnimationDirection.Horizontal,
                    null,
                    (object) true
                        });
                        if (beforeSceneSwitchCallback == null)
                            return;
                        beforeSceneSwitchCallback();
                    }, practice);
                }
                if (buttonNumber == 1)
                {
                    ReflectionUtil.InvokeMethod<object, SinglePlayerLevelSelectionFlowCoordinator>(__instance, "DismissViewController", new object[4]
                    {
                    (object) viewCon,
                    (object) ViewController.AnimationDirection.Horizontal,
                    null,
                    (object) false
                    });
                }

            }));
            ReflectionUtil.InvokeMethod<object, SinglePlayerLevelSelectionFlowCoordinator>(__instance, "PresentViewController", new object[4]
            {
                (object) viewCon,
                null,
                (object) ViewController.AnimationDirection.Horizontal,
                (object) false
            });
        }

        [AffinityPatch(typeof(SinglePlayerLevelSelectionFlowCoordinator), "StartLevelOrShow360Prompt")]
        [AffinityPrefix]
        private bool Prefix(SinglePlayerLevelSelectionFlowCoordinator __instance, Action beforeSceneSwitchCallback, bool practice)
        {
            Plugin.Log.Info("SinglePlayerLevelSelectionFlowCoordinator.StartLevelOrShow360Prompt Prefix");
            if (!RegistrationManager.ScoreSubmissionDisabled(out string message)) return true;

            if (RegistrationManager.ScoreSubmissionDisabled(out string message2))
            {
                ShowWarningText(__instance, beforeSceneSwitchCallback, practice, "CHAT MODIFIERS", $"<align=\"center\"><size=6><size=225%><color=red>Warning!</color></size>\n\nYou have the following ChatModifiers enabled which may disable score submission!\n\n<u>{message2}</u></align>");
            }
            return false;
        }
    }
}
