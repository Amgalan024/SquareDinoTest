using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Player
{
    public class ProjectileModel : MonoBehaviour
    {
        public event Action<Vector3> OnTargetSet;
        public event Action OnReturnedToPool;
        public event Action OnGotFromPool;

        [SerializeField] private float _speed;
        [SerializeField] private int _damage;

        public float Speed => _speed;
        public int Damage => _damage;

        private readonly List<EnemyModel> _affectedEnemies = new List<EnemyModel>();

        public bool IsAvailable { get; private set; }

        public void SetTarget(Vector3 targetPosition)
        {
            OnTargetSet?.Invoke(targetPosition);
        }

        public void ReturnToPool()
        {
            IsAvailable = true;

            OnReturnedToPool?.Invoke();
        }

        public void GetFromPool()
        {
            _affectedEnemies.Clear();

            IsAvailable = false;

            OnGotFromPool?.Invoke();
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