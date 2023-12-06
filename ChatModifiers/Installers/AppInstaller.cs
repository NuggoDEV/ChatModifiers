using ChatModifiers.API;
using Zenject;

namespace ChatModifiers.Installers
{
    internal class AppInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<RegistrationManager>().AsSingle().NonLazy();
        }
    }
}
