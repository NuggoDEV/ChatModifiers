using ChatModifiers.UI;
using ChatModifiers.UI.ModifiersMenuHijacking;
using Zenject;

namespace ChatModifiers.Installers
{
    internal class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CMFlow>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<CMView>().FromNewComponentAsViewController().AsSingle();
            Container.BindInterfacesAndSelfTo<CustomModifierMenuUI>().AsSingle();
        }
    }
}
