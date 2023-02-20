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
        private Dictionary<EnemyModel, Action<int>> _onHealthChangedSubscriptions;
        private Dictionary<EnemyModel, Action> _onEnemyDiedSubscriptions;

        private void Start()
        {
            _camera = Camera.main;

            _enemyHealthBars = new Dictionary<EnemyModel, HealthBar>(_enemyModels.Length);
            _onHealthChangedSubscriptions = new Dictionary<EnemyModel, Action<int>>(_enemyModels.Length);
            _onEnemyDiedSubscriptions = new Dictionary<EnemyModel, Action>(_enemyModels.Length);

            foreach (var enemyModel in _enemyModels)
            {
                var healthBar = Instantiate(enemyModel.HealthBarPrefab, _parentCanvas.transform);

                healthBar.Initialize(enemyModel.HealthPoints);

                _enemyHealthBars.Add(enemyModel, healthBar);

                Action<int> onHealthChanged = value => { HandleEnemyHealthChange(healthBar, value); };
                Action onEnemyDied = () => { healthBar.SetActive(false); };

                enemyModel.OnHealthChanged += onHealthChanged;
                enemyModel.OnEnemyDied += onEnemyDied;

                _onEnemyDiedSubscriptions.Add(enemyModel, onEnemyDied);
                _onHealthChangedSubscriptions.Add(enemyModel, onHealthChanged);
            }
        }

        private void OnDestroy()
        {
            foreach (var enemySubscription in _onHealthChangedSubscriptions)
            {
                enemySubscription.Key.OnHealthChanged -= enemySubscription.Value;
            }

            foreach (var enemySubscription in _onEnemyDiedSubscriptions)
            {
                enemySubscription.Key.OnEnemyDied -= enemySubscription.Value;
            }
        }

        private void Update()
        {
            foreach (var enemyModel in _enemyModels)
            {
                var healthBar = _enemyHealthBars[enemyModel];
                var worldSpaceAnchor = enemyModel.HealthBarAnchor;

                SetHealthBarPosition(healthBar, worldSpaceAnchor);
            }
        }

        private void SetHealthBarPosition(HealthBar healthBar, Transform worldSpaceAnchor)
        {
            healthBar.SetPosition(worldSpaceAnchor.position);

            healthBar.LookAt(healthBar.transform.position + _camera.transform.forward);
        }

        private void HandleEnemyHealthChange(HealthBar healthBar, int currentHealth)
        {
            healthBar.SetValue(currentHealth);
        }
    }
}