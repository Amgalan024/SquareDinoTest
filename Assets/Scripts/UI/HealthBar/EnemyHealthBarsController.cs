using System;
using System.Collections.Generic;
using Enemy;
using Level;
using UnityEngine;

namespace UI
{
    public class EnemyHealthBarsController : MonoBehaviour
    {
        [SerializeField] private Canvas _parentCanvas;

        [SerializeField] private LevelModel _levelModel;

        private Camera _camera;

        private readonly List<EnemyModel> _enemyModels = new List<EnemyModel>();

        private readonly Dictionary<EnemyModel, HealthBar> _enemyHealthBars = new Dictionary<EnemyModel, HealthBar>();

        private readonly Dictionary<EnemyModel, Action<int>> _onHealthChangedSubscriptions =
            new Dictionary<EnemyModel, Action<int>>();

        private readonly Dictionary<EnemyModel, Action>
            _onEnemyDiedSubscriptions = new Dictionary<EnemyModel, Action>();

        private void Awake()
        {
            _camera = Camera.main;

            ExtractEnemyModels();

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

        private void ExtractEnemyModels()
        {
            foreach (var wayPointModel in _levelModel.WayPointModels)
            {
                foreach (var wayPointGoal in wayPointModel.WayPointGoals)
                {
                    if (wayPointGoal.TryGetComponent(out EnemyModel enemyModel))
                    {
                        _enemyModels.Add(enemyModel);
                    }
                }
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