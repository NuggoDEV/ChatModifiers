using ChatModifiers.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
