using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace UI
{
    public class EnemyHealthBarsController : MonoBehaviour
    {
        [SerializeField] private Canvas _parentCanvas;

        [SerializeField] private EnemyModel[] _enemyModels;

        private Camera _camera;

        private Dictionary<EnemyModel, HealthBar> _enemyHealthBars;
        private Dictionary<EnemyModel, Action<int>> _enemySubscriptions;

        private void Start()
        {
            _camera = Camera.main;

            _enemyHealthBars = new Dictionary<EnemyModel, HealthBar>(_enemyModels.Length);
            _enemySubscriptions = new Dictionary<EnemyModel, Action<int>>(_enemyModels.Length);

            foreach (var enemyModel in _enemyModels)
            {
                var healthBar = Instantiate(enemyModel.HealthBarPrefab, _parentCanvas.transform);

                healthBar.Initialize(enemyModel.HealthPoints);

                _enemyHealthBars.Add(enemyModel, healthBar);

                Action<int> subscription = value => { HandleEnemyHealthChange(healthBar, value); };

                enemyModel.OnHealthChanged += subscription;

                _enemySubscriptions.Add(enemyModel, subscription);
            }
        }

        private void OnDestroy()
        {
            foreach (var enemySubscription in _enemySubscriptions)
            {
                enemySubscription.Key.OnHealthChanged -= enemySubscription.Value;
            }
        }

        private void Update()
        {
            foreach (var enemyModel in _enemyModels)
            {
                var healthBar = _enemyHealthBars[enemyModel];
                var anchor = enemyModel.HealthBarAnchor;

                SetHealthBarPosition(healthBar, anchor);
            }
        }

        private void SetHealthBarPosition(HealthBar healthBar, Transform anchor)
        {
            var enemyScreenPosition = _camera.WorldToScreenPoint(anchor.position);

            healthBar.transform.position = enemyScreenPosition;
        }

        private void HandleEnemyHealthChange(HealthBar healthBar, int currentHealth)
        {
            healthBar.SetValue(currentHealth);
        }
    }
}