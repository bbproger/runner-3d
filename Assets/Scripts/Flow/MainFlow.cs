using Ui;
using Ui.Views;
using UnityEngine;
using Zenject;

namespace Flow
{
    public class MainFlow : IInitializable
    {
        private GameplayFlow _gameplayFlow;
        private PresenterService _presenterService;

        public void Initialize()
        {
            Application.targetFrameRate = 60;
            ShowMainMenu();
        }

        [Inject]
        private void Inject(PresenterService presenterService, GameplayFlow gameplayFlow)
        {
            _gameplayFlow = gameplayFlow;
            _presenterService = presenterService;
        }

        private void ShowMainMenu()
        {
            _presenterService.Show<MainMenuView>();
        }

        public void StartGame()
        {
            _gameplayFlow.StartGame();
        }
    }
}