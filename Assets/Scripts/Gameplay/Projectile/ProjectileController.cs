using System;
using Projectile.Behaviours;
using UnityEngine;

namespace Projectile
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private ProjectileModel _projectileModel;
        [SerializeField] private ProjectileView _projectileView;
        [SerializeField] private BaseProjectileBehaviour _projectileBehaviour;

        private Action _returnToPool;
        private Action _takeFromPool;

        private void Awake()
        {
            _takeFromPool = () => _projectileView.SetActive(true);
            _returnToPool = () => _projectileView.SetActive(false);

            _projectileView.OnHit += _projectileBehaviour.HandleProjectileHit;
            _projectileModel.OnProjectileSetup += _projectileBehaviour.HandleProjectileSetup;

            _projectileModel.OnTakenFromPool += _takeFromPool;
            _projectileModel.OnReturnedToPool += _returnToPool;
        }

        private void OnDestroy()
        {
            _projectileView.OnHit -= _projectileBehaviour.HandleProjectileHit;
            _projectileModel.OnProjectileSetup -= _projectileBehaviour.HandleProjectileSetup;

            _projectileModel.OnTakenFromPool -= _takeFromPool;
            _projectileModel.OnReturnedToPool -= _returnToPool;
        }
    }
}