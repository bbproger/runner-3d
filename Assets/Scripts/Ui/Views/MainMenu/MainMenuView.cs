using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Views
{
    public class MainMenuView : BasePresenterWithController<MainMenuViewController>
    {
        [SerializeField] private Button startButton;

        public override void Show()
        {
            base.Show();
            startButton.OnClickAsObservable().Subscribe(_ => PresenterController.StartGame()).AddTo(Disposables);
        }
    }
}