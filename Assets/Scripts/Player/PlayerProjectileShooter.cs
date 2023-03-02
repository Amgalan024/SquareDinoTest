using Projectile;
using UnityEngine;

namespace Player
{
    public class PlayerProjectileShooter : MonoBehaviour
    {
        [SerializeField] private ProjectilePool _projectilePool;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _distanceMultiplierOnMiss = 25f;
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private string _layerName;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _projectilePool.Initialize(_playerModel.ProjectilesRoot);
        }

        public void ShootProjectile(Vector3 touchPosition)
        {
            var projectile = _projectilePool.GetProjectile();

            var targetPosition = GetTargetPosition(touchPosition);

            projectile.SetupProjectile(_shootPoint.position, targetPosition);
        }

        private Vector3 GetTargetPosition(Vector3 touchPosition)
        {
            var nearPos = new Vector3(touchPosition.x, touchPosition.y, _camera.nearClipPlane);
            var farPos = new Vector3(touchPosition.x, touchPosition.y, _camera.farClipPlane);

            var nearPosConverted = _camera.ScreenToWorldPoint(nearPos);
            var farPosConverted = _camera.ScreenToWorldPoint(farPos);

            var missedDirection = nearPosConverted + farPosConverted * _distanceMultiplierOnMiss;

            if (Physics.Raycast(nearPosConverted, farPosConverted - nearPosConverted, out RaycastHit hit,
                    Mathf.Infinity, ~(LayerMask.GetMask(_layerName))))
            {
                return hit.point;
            }
            else
            {
                return missedDirection;
            }
        }
    }
}