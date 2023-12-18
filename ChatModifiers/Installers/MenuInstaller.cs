using ChatModifiers.UI;
using ChatModifiers.UI.MainMenu;
using ChatModifiers.UI.ModifiersMenuHijacking;
using ChatModifiers.Utilities;
using Zenject;

namespace ChatModifiers.Installers
{
    internal class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<CMView>().FromNewComponentAsViewController().AsSingle();
            Container.BindInterfacesAndSelfTo<CMFlow>().FromNewComponentOnNewGameObject().AsSingle();
            Container.BindInterfacesAndSelfTo<CustomModifierMenuUI>().AsSingle();
            Container.BindInterfacesAndSelfTo<WarningHandler>().AsSingle();
        }
    }
}
