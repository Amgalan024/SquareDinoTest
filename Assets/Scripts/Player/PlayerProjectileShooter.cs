using System;
using UnityEngine;

namespace Player
{
    public class PlayerProjectileShooter : MonoBehaviour
    {
        [SerializeField] private ProjectilePool _projectilePool;
        [SerializeField] private Transform _shootPoint;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void ShootProjectile(Vector3 touchPosition)
        {
            var projectile = _projectilePool.GetProjectile();

            projectile.transform.position = _shootPoint.position;

            var targetPosition = GetTargetPosition(touchPosition);

            projectile.SetTarget(targetPosition);
        }

        private Vector3 GetTargetPosition(Vector3 touchPosition)
        {
            var nearPos = new Vector3(touchPosition.x, touchPosition.y, _camera.nearClipPlane);
            var farPos = new Vector3(touchPosition.x, touchPosition.y, _camera.farClipPlane);

            var nearPosConverted = _camera.ScreenToWorldPoint(nearPos);
            var farPosConverted = _camera.ScreenToWorldPoint(farPos);

            Vector3 direction;

            if (Physics.Raycast(nearPosConverted, farPosConverted - nearPosConverted, out RaycastHit hit,
                    Mathf.Infinity))
            {
                direction = hit.point;
            }
            else
            {
                direction = nearPosConverted + farPosConverted * 25;
            }

            return direction;
        }
    }
}