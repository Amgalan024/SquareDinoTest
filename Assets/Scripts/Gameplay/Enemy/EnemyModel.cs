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
        [SerializeField] private float _deathDuration;
        [SerializeField] private float _reviveDuration;

        public int HealthPoints => _healthPoints;
        public float DeathDuration => _deathDuration;
        public float ReviveDuration => _reviveDuration;


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