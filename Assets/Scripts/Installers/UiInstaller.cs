using Ui;
using Ui.Popups;
using Ui.Views;
using Ui.Views.Gameplay;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private PresenterService presenterService;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PresenterService>().FromInstance(presenterService).AsSingle().NonLazy();
            Container.BindFactory<MainMenuViewController, PresenterController<MainMenuViewController>.Factory>()
                .AsSingle().Lazy();
            Container.BindFactory<GameplayViewController, PresenterController<GameplayViewController>.Factory>()
                .AsSingle().Lazy();

            Container.BindFactory<AlertPopupController, PresenterController<AlertPopupController>.Factory>();
        }
    }
}