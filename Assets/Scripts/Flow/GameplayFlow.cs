using System;
using Gameplay;
using Ui;
using Ui.Popups;
using Ui.Views.Gameplay;
using UniRx;
using Zenject;
using Object = UnityEngine.Object;

namespace Flow
{
    public class GameplayFlow
    {
        private CompositeDisposable _disposables;

        private GameplayController _gameplayController;
        private GameplayController _gameplayControllerPrefab;
        private MainFlow _mainFlow;
        private PresenterService _presenterService;

        [Inject]
        private void Inject(GameplayController gameplayControllerPrefab, PresenterService presenterService,
            MainFlow mainFlow)
        {
            _mainFlow = mainFlow;
            _presenterService = presenterService;
            _gameplayControllerPrefab = gameplayControllerPrefab;
        }

        public void StartGame()
        {
            _disposables = new CompositeDisposable();
            _gameplayController = Object.Instantiate(_gameplayControllerPrefab);

            _gameplayController.StartGame();
            _presenterService.Show<GameplayView>();
            _gameplayController.OnGameEndObservable.Subscribe(LeaveGame).AddTo(_disposables);
        }

        public void LeaveGame(GameEndReason reason)
        {
            switch (reason)
            {
                case GameEndReason.LeaveGame:
                    Object.Destroy(_gameplayController.gameObject);
                    _mainFlow.Initialize();
                    break;
                case GameEndReason.MissionComplete:
                    _presenterService.Show<AlertPopup>(new AlertPopupData
                    {
                        AlertButton = AlertPopupButton.Ok,
                        Description = "Congratulations!!! Mission completed",
                        OnActionComplete = _ =>
                        {
                            Object.Destroy(_gameplayController.gameObject);
                            _mainFlow.Initialize();
                        }
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reason), reason, null);
            }
        }
    }
}