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
        [SerializeField] private HealthBar _healthBarPrefab;
        [SerializeField] private Transform _healthBarAnchor;

        public int HealthPoints => _healthPoints;
        public HealthBar HealthBarPrefab => _healthBarPrefab;
        public Transform HealthBarAnchor => _healthBarAnchor;

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