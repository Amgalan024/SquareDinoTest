using System;
using UI;
using UnityEngine;

namespace Enemy
{
    public class EnemyModel : MonoBehaviour
    {
        public event Action<int> OnHealthChanged;
        public event Action OnEnemyDied;

        [SerializeField] private int _healthPoints;

        public int HealthPoints => _healthPoints;

        private int _currentHealthPoints;

        private void Start()
        {
            _currentHealthPoints = _healthPoints;
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