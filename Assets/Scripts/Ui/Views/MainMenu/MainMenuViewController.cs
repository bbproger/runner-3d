using Flow;
using Zenject;

namespace Ui.Views
{
    public class MainMenuViewController : PresenterController<MainMenuViewController>
    {
        private MainFlow _mainFlow;

        [Inject]
        private void Inject(MainFlow mainFlow)
        {
            _mainFlow = mainFlow;
        }

        public void StartGame()
        {
            _mainFlow.StartGame();
        }
    }
}