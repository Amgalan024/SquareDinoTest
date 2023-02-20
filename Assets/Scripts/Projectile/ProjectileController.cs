using System;
using UnityEngine;

namespace Projectile
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private ProjectileModel _projectileModel;
        [SerializeField] private ProjectileView _projectileView;

        private Action _returnToPool;
        private Action _takeFromPool;

        private void Awake()
        {
            _takeFromPool = () => _projectileView.SetActive(true);
            _returnToPool = () => _projectileView.SetActive(false);

            _projectileView.OnHit += HandleHit;

            _projectileModel.OnProjectileSetup += HandleProjectileSetup;

            _projectileModel.OnTakenFromPool += _takeFromPool;
            _projectileModel.OnReturnedToPool += _returnToPool;
        }

        private void OnDestroy()
        {
            _projectileView.OnHit -= HandleHit;

            _projectileModel.OnProjectileSetup -= HandleProjectileSetup;

            _projectileModel.OnTakenFromPool -= _takeFromPool;
            _projectileModel.OnReturnedToPool -= _returnToPool;
        }

        private void HandleHit(Collision collision)
        {
            _projectileModel.ReturnToPool();
        }

        private void HandleProjectileSetup()
        {
            _projectileView.SetPosition(_projectileModel.ShootPosition);
            _projectileView.LookAt(_projectileModel.TargetPosition);
            _projectileView.SetSpeed(_projectileModel.Speed);
        }
    }
}