﻿using Player;
using UnityEngine;
using WayPoint;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private EnemyModel _enemyModel;
        [SerializeField] private WayPointGoalModel _wayPointGoalModel;

        private void Start()
        {
            _enemyView.OnHit += HandleEnemyHit;
            _enemyModel.OnEnemyDied += HandleEnemyDeath;
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void Dispose()
        {
            _enemyView.OnHit -= HandleEnemyHit;
            _enemyModel.OnEnemyDied -= HandleEnemyDeath;
        }

        private void HandleEnemyHit(EnemyView enemyView, Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out ProjectileModel projectileModel))
            {
                if (!projectileModel.ContainsAffectedEnemy(_enemyModel))
                {
                    projectileModel.AddAffectedEnemy(_enemyModel);
                    _enemyModel.TakeDamage(projectileModel.Damage);
                }
            }
        }

        private void HandleEnemyDeath()
        {
            _enemyView.PlayDeathAnimation();

            _wayPointGoalModel.InvokeGoalAchievement();

            Dispose();
        }
    }
}