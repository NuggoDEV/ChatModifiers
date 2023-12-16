using ChatModifiers.Utilities;
using Zenject;
using Installer = Zenject.Installer;

namespace ChatModifiers.Installers
{
    public partial class GameCoreInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<NotificationController>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}
