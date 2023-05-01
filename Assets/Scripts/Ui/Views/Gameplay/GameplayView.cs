using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Views.Gameplay
{
    public class GameplayView : BasePresenterWithController<GameplayViewController>
    {
        [SerializeField] private Button leaveButton;

        public override void Show()
        {
            base.Show();
            leaveButton.OnClickAsObservable().Subscribe(_ => PresenterController.LeaveGame()).AddTo(Disposables);
        }
    }
}