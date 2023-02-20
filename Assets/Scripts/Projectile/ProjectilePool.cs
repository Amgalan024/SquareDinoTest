using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private ProjectileModel _projectilePrefab;
        [SerializeField] private int _initialCount;

        private List<ProjectileModel> _availableProjectiles;

        private void Start()
        {
            _availableProjectiles = new List<ProjectileModel>(_initialCount);

            for (int i = 0; i < _initialCount; i++)
            {
                var projectile = Instantiate(_projectilePrefab, transform);

                projectile.ReturnToPool();

                _availableProjectiles.Add(projectile);
            }
        }

        public ProjectileModel GetProjectile()
        {
            var pooledProjectile = _availableProjectiles.FirstOrDefault(p => p.IsAvailable);

            if (pooledProjectile == null)
            {
                pooledProjectile = Instantiate(_projectilePrefab);

                _availableProjectiles.Add(pooledProjectile);
            }

            pooledProjectile.GetFromPool();

            return pooledProjectile;
        }
    }
}