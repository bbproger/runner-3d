using System;
using Gameplay.Weapons.Bullets;
using UniRx;
using UnityEngine;

namespace Gameplay.Player
{
    public abstract class AbstractEnemy : MonoBehaviour, IDamagable
    {
        [SerializeField] private float health;
        private BehaviorSubject<float> _health;
        public IObservable<Unit> OnDieObservable => _health.AsUnitObservable();

        private void Awake()
        {
            _health = new BehaviorSubject<float>(health);
        }

        public void TakeDamage(float damageAmount)
        {
            _health.OnNext(_health.Value - damageAmount);

            if (_health.Value <= 0)
            {
                _health.OnCompleted();
                gameObject.SetActive(false);
            }
        }
    }
}