using Flow;
using Services;
using Zenject;

namespace Installers
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainFlow>().AsSingle().NonLazy();
            Container.Bind<GameplayFlow>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();
        }
    }
}