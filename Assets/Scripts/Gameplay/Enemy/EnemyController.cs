using System;
using Cysharp.Threading.Tasks;
using Projectile;
using UnityEngine;
using WayPoint;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private EnemyModel _enemyModel;
        [SerializeField] private WayPointGoalModel _wayPointGoalModel;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;

            _enemyView.HealthBar.Initialize(_enemyModel.HealthPoints);

            _enemyView.OnHit += HandleEnemyHit;
            _enemyModel.OnEnemyDied += HandleEnemyDeath;
            _enemyModel.OnHealthChanged += _enemyView.SetHealthBarValue;
        }

        private void Update()
        {
            _enemyView.HealthBar.LookAt(_enemyView.HealthBar.transform.position + _camera.transform.forward);
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void Dispose()
        {
            _enemyView.OnHit -= HandleEnemyHit;
            _enemyModel.OnEnemyDied -= HandleEnemyDeath;
            _enemyModel.OnHealthChanged -= _enemyView.SetHealthBarValue;
        }

        private void HandleEnemyHit(EnemyView enemyView, Collider otherCollider)
        {
            if (otherCollider.attachedRigidbody.TryGetComponent(out ProjectileModel projectileModel))
            {
                if (!projectileModel.ContainsAffectedEnemy(_enemyModel))
                {
                    HandleEnemyHitAsync(projectileModel).Forget();
                }
            }
        }

        private async UniTask HandleEnemyHitAsync(ProjectileModel projectileModel)
        {
            _enemyView.PlayDeathAnimation();

            projectileModel.AddAffectedEnemy(_enemyModel);
            _enemyModel.TakeDamage(projectileModel.Damage);

            await UniTask.Delay(TimeSpan.FromSeconds(_enemyModel.DeathDuration));

            if (_enemyModel.HealthPoints > 0)
            {
                _enemyView.PlayReviveAnimationAsync(_enemyModel.ReviveDuration).Forget();
            }
        }

        private void HandleEnemyDeath()
        {
            _enemyView.HealthBar.SetActive(false);

            _wayPointGoalModel.InvokeGoalAchievement();

            Dispose();
        }
    }
}