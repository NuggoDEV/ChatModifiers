using ChatModifiers.Installers;
using ChatModifiers.Utilities;
using ChatModifiers.UI;
using SiraUtil.Zenject;
using IPA;
using IPALogger = IPA.Logging.Logger;


namespace ChatModifiers
{
    [Plugin(RuntimeOptions.SingleStartInit), NoEnableDisable]
    public class Plugin
    {
        public static IPALogger Log { get; private set; }

        [Init]
        public Plugin(IPALogger logger, Zenjector zenjector)
        {
            Log = logger;

            zenjector.UseLogger(logger);
            zenjector.Install<AppInstaller>(Location.App);
            zenjector.Install<MenuInstaller>(Location.Menu);

            TwitchConnection.Initialize();
            CMFlow.CreateMenuButton();
        }
    }
}
