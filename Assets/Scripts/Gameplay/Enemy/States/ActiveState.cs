using System;
using Cysharp.Threading.Tasks;
using Gameplay.GameplayStates;
using Player;
using Projectile;
using UnityEngine;

namespace Enemy.States
{
    public class ActiveState : BaseState
    {
        [SerializeField] private EnemyModel _enemyModel;
        [SerializeField] private EnemyView _enemyView;

        public override void Enter()
        {
            _enemyView.MoveTo(_enemyModel.Target.position);

            _enemyView.HealthBar.Initialize(_enemyModel.StartHealthPoints);

            _enemyView.OnHit += HandleEnemyHit;
            _enemyModel.OnEnemyDied += HandleEnemyDeath;
            _enemyModel.OnHealthChanged += _enemyView.SetHealthBarValue;
        }

        public override void Exit()
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

            if (otherCollider.attachedRigidbody.TryGetComponent(out PlayerModel playerModel))
            {
                StateMachine.ChangeState<InactiveState>();
            }
        }

        private async UniTask HandleEnemyHitAsync(ProjectileModel projectileModel)
        {
            _enemyView.PlayDeathAnimation();

            projectileModel.AddAffectedEnemy(_enemyModel);
            _enemyModel.TakeDamage(projectileModel.Damage);

            await UniTask.Delay(TimeSpan.FromSeconds(_enemyModel.DeathDuration));

            if (_enemyModel.IsAlive)
            {
                _enemyView.PlayReviveAnimationAsync(_enemyModel.ReviveDuration).Forget();
            }
        }

        private void HandleEnemyDeath()
        {
            StateMachine.ChangeState<DeathState>();
        }
    }
}