using ChatModifiers.Installers;
using ChatModifiers.UI;
using ChatModifiers.Utilities;
using IPA;
using SiraUtil.Zenject;
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
            Config.instance = new Config();
            Config.instance.Load();
            zenjector.UseLogger(logger);
            zenjector.Install<AppInstaller>(Location.App);
            zenjector.Install<MenuInstaller>(Location.Menu);

            TwitchConnection.Initialize();
            CMFlow.CreateMenuButton();
        }
    }
}
