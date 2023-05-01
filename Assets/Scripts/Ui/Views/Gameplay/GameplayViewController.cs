using Flow;
using Gameplay;
using Zenject;

namespace Ui.Views.Gameplay
{
    public class GameplayViewController : PresenterController<GameplayViewController>
    {
        private GameplayFlow _gameplayFlow;

        [Inject]
        private void Inject(GameplayFlow gameplayFlow)
        {
            _gameplayFlow = gameplayFlow;
        }

        public void LeaveGame()
        {
            _gameplayFlow.LeaveGame(GameEndReason.LeaveGame);
        }
    }
}