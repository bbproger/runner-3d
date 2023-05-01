using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Gameplay.Player
{
    public class EnemiesGroup : MonoBehaviour
    {
        [SerializeField] private AbstractEnemy[] enemies;
        public IObservable<Unit> OnAllEnemiesDieObservable => enemies.Select(enemy => enemy.OnDieObservable).WhenAll();
    }
}