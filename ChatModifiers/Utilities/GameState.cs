using HarmonyLib;
using IPA.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace ChatModifiers.Utilities
{
    internal static class GameState
    {
        internal static bool IsInGame = false;
        internal static bool inReplay = false;

        internal static void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            if (arg1.name == "GameCore")
            {
                IsInGame = true;
                var scoresaber = PluginManager.GetPluginFromId("ScoreSaber");
                if (scoresaber != null)
                {
                    MethodBase ScoreSaber_playbackEnabled = AccessTools.Method("ScoreSaber.Core.ReplaySystem.HarmonyPatches.PatchHandleHMDUnmounted:Prefix");
                    if (ScoreSaber_playbackEnabled != null && (bool)ScoreSaber_playbackEnabled.Invoke(null, null) == false)
                    {
                        inReplay = true;
                    }
                }

                var beatleader = PluginManager.GetPluginFromId("BeatLeader");
                if (beatleader != null)
                {
                    var _replayStarted = beatleader?.Assembly.GetType("BeatLeader.Replayer.ReplayerLauncher")?
                    .GetProperty("IsStartedAsReplay", BindingFlags.Static | BindingFlags.Public);
                    if (_replayStarted != null && (bool)_replayStarted.GetValue(null, null))
                    {
                        inReplay = true;
                    }
                }
            }
            else
            {
                IsInGame = false;
                inReplay = false;
            }
            if (Environment.GetCommandLineArgs().Any(x => x.ToLower() == "--verbose"))
            {
                inReplay = false;
            }
        }
    }
}
