using DefaultNamespace.Ui;
using UniRx;
using Zenject;

namespace Ui
{
    public class BasePresenterWithController<TController> : BasePresenter
        where TController : PresenterController<TController>
    {
        private PresenterController<TController>.Factory _presenterControllerFactory;
        protected TController PresenterController;

        [Inject]
        private void Inject(PresenterController<TController>.Factory presenterControllerFactory)
        {
            _presenterControllerFactory = presenterControllerFactory;
        }

        public override void Setup(IPresenterData data)
        {
            base.Setup(data);

            PresenterController = _presenterControllerFactory.Create().AddTo(Disposables);
            PresenterController.Setup(data);
        }
    }
}