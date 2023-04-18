using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Projectile
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private ProjectileModel _projectilePrefab;
        [SerializeField] private int _initialCount;

        private List<ProjectileModel> _availableProjectiles;
        private Transform _root;

        public void Initialize(Transform root)
        {
            _root = root;
            
            _availableProjectiles = new List<ProjectileModel>(_initialCount);

            for (int i = 0; i < _initialCount; i++)
            {
                var projectile = Instantiate(_projectilePrefab, _root);

                projectile.ReturnToPool();

                _availableProjectiles.Add(projectile);
            }
        }

        public ProjectileModel GetProjectile()
        {
            var pooledProjectile = _availableProjectiles.FirstOrDefault(p => p.IsAvailable);

            if (pooledProjectile == null)
            {
                pooledProjectile = Instantiate(_projectilePrefab, _root);

                _availableProjectiles.Add(pooledProjectile);
            }

            pooledProjectile.TakeFromPool();

            return pooledProjectile;
        }
    }
}