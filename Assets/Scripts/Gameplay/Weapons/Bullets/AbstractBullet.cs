using System;
using Gameplay.Pool;
using UniRx;
using UnityEngine;

namespace Gameplay.Weapons.Bullets
{
    public abstract class AbstractBullet : MonoBehaviour, IPoolObject
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private float damageAmount;
        [SerializeField] private float speed;
        [SerializeField] private float lifeTime;
        private Action _onCompleted;
        private IDisposable _timerDisposable;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out IDamagable damagable))
            {
                return;
            }

            damagable.TakeDamage(damageAmount);
            Complete();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            rigidbody.velocity = Vector3.zero;
            gameObject.SetActive(false);
            _timerDisposable?.Dispose();
        }

        public void Initialize(Action onCompleted)
        {
            _onCompleted = onCompleted;
            _timerDisposable = Observable.Timer(TimeSpan.FromSeconds(lifeTime)).Subscribe(_ => Complete());
        }

        public void AddForce(Vector3 direction)
        {
            rigidbody.velocity = transform.TransformDirection(direction * speed);
        }

        private void Complete()
        {
            _onCompleted?.Invoke();
        }

        private void OnDestroy()
        {
            _timerDisposable?.Dispose();
        }
    }
}