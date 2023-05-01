using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Services
{
    public class InputService : IInitializable
    {
        public IObservable<Vector3> MouseDownObservable { get; private set; }

        public void Initialize()
        {
            MouseDownObservable = Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Select(_ => Input.mousePosition);
        }
    }
}