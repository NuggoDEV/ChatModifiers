using ChatModifiers.Installers;
using ChatModifiers.Utilities;
using IPA;
using IPA.Utilities;
using SiraUtil.Zenject;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace ChatModifiers
{
    [Plugin(RuntimeOptions.SingleStartInit), NoEnableDisable]
    public class Plugin
    {
        public static IPALogger Log { get; private set; }
        internal static bool debug = true;

        [Init]
        public Plugin(IPALogger logger, Zenjector zenjector)
        {
            Log = logger;
            Config.Instance = new Config();
            Config.Instance.Load();
            zenjector.UseLogger(logger);
            zenjector.Install<AppInstaller>(Location.App);
            zenjector.Install<MenuInstaller>(Location.Menu);
            zenjector.Install<GameCoreInstaller>(Location.GameCore);
            SceneManager.activeSceneChanged += GameState.SceneManager_activeSceneChanged;
            TwitchConnection.Initialize();
        }
    }
}
