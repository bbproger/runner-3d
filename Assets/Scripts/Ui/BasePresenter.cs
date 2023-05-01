using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Ui
{
    [RequireComponent(typeof(ZenAutoInjecter))]
    public class BasePresenter : MonoBehaviour
    {
        protected CompositeDisposable Disposables;

        private void OnDestroy()
        {
            Disposables?.Dispose();
        }

        public virtual void Setup(IPresenterData data = null)
        {
            Disposables = new CompositeDisposable();
        }

        public virtual void Show()
        {
        }

        public virtual void Close()
        {
        }
    }
}