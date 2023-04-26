using DG.Tweening;
using UnityEngine;

namespace Projectile.Behaviours
{
    public class KnifeBehaviour : BaseProjectileBehaviour
    {
        [SerializeField] private ProjectileView _projectileView;
        [SerializeField] private ProjectileModel _projectileModel;
        [SerializeField] private Transform _visualization;
        [SerializeField] private float _penetration;
        [SerializeField] private float _onHitAngle;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        private Tween _movementTween;
        private Sequence _rotationTween;

        public override void HandleProjectileHit(Collider otherCollider)
        {
            _movementTween?.Kill();
            _rotationTween?.Kill();

            _collider.enabled = false;

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.isKinematic = true;

            var rotationEulerAngles = transform.rotation.eulerAngles;
            rotationEulerAngles.x = _onHitAngle;

            _projectileView.SetParent(otherCollider.transform);

            _visualization.eulerAngles = (Quaternion.identity.eulerAngles);
            _visualization.eulerAngles = (rotationEulerAngles);
            
            var penetrationVector = _visualization.forward * _penetration;
            _projectileView.SetPosition(transform.position + penetrationVector);
        }
    
        public override void SetupBehaviour()
        {
            _projectileView.SetPosition(_projectileModel.ShootPosition);
            _projectileView.LookAt(_projectileModel.TargetPosition);
            
            SetupMovementTween();
            SetupRotationTween();
        }

        private void SetupMovementTween()
        {
            var distance = (transform.position - _projectileModel.TargetPosition).magnitude;
            var time = distance / _movementSpeed;
            _movementTween = transform.DOMove(_projectileModel.TargetPosition, time);
        }

        private void SetupRotationTween()
        {
            _rotationTween = DOTween.Sequence();

            var rotationEulerAngles = _visualization.rotation.eulerAngles;
            rotationEulerAngles.x = 360;
            _rotationTween.Append(_visualization.DORotate(rotationEulerAngles, _rotationSpeed, RotateMode.FastBeyond360));
            _rotationTween.SetLoops(-1);
        }
    }
}