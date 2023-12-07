using ChatModifiers.Utilities;
using System.Configuration.Install;
using Zenject;
using static ChatModifiers.Utilities.GameCoreUtils;
using Installer = Zenject.Installer;

namespace ChatModifiers.Installers
{
    public partial class GameCoreInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<GameCoreUtils>().AsSingle().NonLazy();
            Container.Bind<NotificationController>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}
