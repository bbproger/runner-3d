using System;
using DefaultNamespace.Ui;
using UniRx;
using Zenject;

namespace Ui
{
    public abstract class PresenterController<TController> : IDisposable
    {
        protected readonly CompositeDisposable CompositeDisposable = new();

        public void Dispose()
        {
            CompositeDisposable.Dispose();
        }

        public virtual void Setup(IPresenterData data = null)
        {
        }

        public virtual void Show()
        {
        }

        public class Factory : PlaceholderFactory<TController>
        {
        }
    }
}