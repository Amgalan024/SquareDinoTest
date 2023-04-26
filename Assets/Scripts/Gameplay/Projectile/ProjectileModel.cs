using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Projectile
{
    public class ProjectileModel : MonoBehaviour
    {
        public event Action OnSetup;
        public event Action OnReturnedToPool;
        public event Action OnTakenFromPool;

        [SerializeField] private float _speed;
        [SerializeField] private int _damage;

        public bool IsAvailable { get; private set; }
        public Vector3 ShootPosition { get; private set; }
        public Vector3 TargetPosition { get; private set; }

        public float Speed => _speed;
        public int Damage => _damage;

        private readonly List<EnemyModel> _affectedEnemies = new List<EnemyModel>();

        public void Setup(Vector3 shootPosition, Vector3 targetPosition)
        {
            ShootPosition = shootPosition;
            TargetPosition = targetPosition;

            OnSetup?.Invoke();
        }

        public void ReturnToPool()
        {
            IsAvailable = true;

            OnReturnedToPool?.Invoke();
        }

        public void TakeFromPool()
        {
            _affectedEnemies.Clear();

            IsAvailable = false;

            OnTakenFromPool?.Invoke();
        }

        public void AddAffectedEnemy(EnemyModel enemyModel)
        {
            _affectedEnemies.Add(enemyModel);
        }

        public bool ContainsAffectedEnemy(EnemyModel enemyModel)
        {
            return _affectedEnemies.Contains(enemyModel);
        }
    }
}