using ChatModifiers.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace ChatModifiers.Installers
{
    internal class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<CMFlow>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<CMView>().FromNewComponentAsViewController().AsSingle();
        }
    }
}
