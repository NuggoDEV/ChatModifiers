using ChatModifiers.API;
using ChatModifiers.Installers;
using ChatModifiers.UI;
using ChatModifiers.Modifiers;
using IPA;
using SiraUtil.Zenject;
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
            
            zenjector.Install<AppInstaller>(Location.App);
            zenjector.Install<MenuInstaller>(Location.Menu);
            
            CMFlow.CreateMenuButton();
            ModifierManager.RegisterPluginModifiers();
        }
    }
}
