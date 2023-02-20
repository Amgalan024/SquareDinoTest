using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private ProjectileView _projectilePrefab;
        [SerializeField] private int _initialCount;

        private List<ProjectileView> _availableProjectiles;

        private void Start()
        {
            _availableProjectiles = new List<ProjectileView>(_initialCount);

            for (int i = 0; i < _initialCount; i++)
            {
                var projectile = Instantiate(_projectilePrefab, transform);

                projectile.ReturnToPool();

                _availableProjectiles.Add(projectile);
            }
        }

        public ProjectileView GetProjectile()
        {
            var pooledProjectile = _availableProjectiles.FirstOrDefault(p => p.gameObject.activeInHierarchy);

            if (pooledProjectile == null)
            {
                pooledProjectile = Instantiate(_projectilePrefab);

                _availableProjectiles.Add(pooledProjectile);
            }

            pooledProjectile.Initialize();

            return pooledProjectile;
        }
    }
}