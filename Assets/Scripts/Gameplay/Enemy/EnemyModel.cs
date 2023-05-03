using System;
using Level;
using UI;
using UnityEngine;

namespace Enemy
{
    public class EnemyModel : MonoBehaviour
    {
        public event Action<int> OnHealthChanged;
        public event Action OnEnemyDied;

        [SerializeField] private int _startHealthPoints;
        [SerializeField] private float _deathDuration;
        [SerializeField] private float _reviveDuration;

        public Transform Target { get; private set; }
        public int StartHealthPoints => _startHealthPoints;
        public float DeathDuration => _deathDuration;
        public float ReviveDuration => _reviveDuration;
        public bool IsAlive => _currentHealthPoints > 0;

        private int _currentHealthPoints;

        public void Initialize(Transform target)
        {
            _currentHealthPoints = _startHealthPoints;
            Target = target;
        }

        public void TakeDamage(int damage)
        {
            _currentHealthPoints -= damage;

            OnHealthChanged?.Invoke(_currentHealthPoints);

            if (_currentHealthPoints <= 0)
            {
                _currentHealthPoints = 0;
                OnEnemyDied?.Invoke();
            }
        }
    }
}