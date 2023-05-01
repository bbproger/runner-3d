using System;
using System.Collections.Generic;

namespace Gameplay.Pool
{
    public class Pool<T> : IDisposable where T : class, IPoolObject
    {
        private readonly Func<T> _creator;
        private readonly Queue<IPoolObject> _pool;

        public Pool(Func<T> creator, int initialAmount = 0)
        {
            _creator = creator;
            _pool = new Queue<IPoolObject>();

            for (int i = 0; i < initialAmount; i++)
            {
                T createdObject = _creator.Invoke();
                Consume(createdObject);
            }
        }

        public void Dispose()
        {
            _pool.Clear();
        }

        public T GetObject()
        {
            if (_pool.Count == 0)
            {
                T createdObject = _creator.Invoke();
                createdObject?.Activate();
                return createdObject;
            }

            T poolObject = _pool.Dequeue() as T;
            poolObject.Activate();
            return poolObject;
        }

        public void Consume(T obj)
        {
            obj.Deactivate();
            _pool.Enqueue(obj);
        }
    }
}