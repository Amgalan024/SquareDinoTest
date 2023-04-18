using DG.Tweening;
using UnityEngine;

namespace Projectile.Behaviours
{
    public class BulletBehaviour : BaseProjectileBehaviour
    {
        [SerializeField] private ProjectileModel _projectileModel;
        [SerializeField] private ProjectileView _projectileView;
        [SerializeField] private float _movementSpeed;

        private Tween _movementTween;

        public override void HandleProjectileHit(Collider otherCollider)
        {
            _movementTween?.Kill();
            _projectileModel.ReturnToPool();
        }

        public override void HandleProjectileSetup()
        {
            _projectileView.SetPosition(_projectileModel.ShootPosition);
            _projectileView.LookAt(_projectileModel.TargetPosition);
            SetupMovementTween();
        }

        private void SetupMovementTween()
        {
            var distance = (transform.position - _projectileModel.TargetPosition).magnitude;
            var time = distance / _movementSpeed;
            _movementTween = transform.DOMove(_projectileModel.TargetPosition, time);
        }
    }
}