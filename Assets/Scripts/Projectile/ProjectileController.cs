using System;
using UnityEngine;

namespace Player
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private ProjectileModel _projectileModel;
        [SerializeField] private ProjectileView _projectileView;

        private Action _returnToPool;
        private Action _getFromPool;

        private void Awake()
        {
            _getFromPool = () => _projectileView.SetActive(true);
            _returnToPool = () => _projectileView.SetActive(false);

            _projectileView.OnHit += HandleHit;

            _projectileModel.OnTargetSet += HandleTargetSet;

            _projectileModel.OnGotFromPool += _getFromPool;
            _projectileModel.OnReturnedToPool += _returnToPool;
        }

        private void OnDestroy()
        {
            _projectileView.OnHit -= HandleHit;

            _projectileModel.OnTargetSet -= HandleTargetSet;

            _projectileModel.OnGotFromPool -= _getFromPool;
            _projectileModel.OnReturnedToPool -= _returnToPool;
        }

        private void HandleHit(Collision collision)
        {
            _projectileModel.ReturnToPool();
        }

        private void HandleTargetSet(Vector3 targetPosition)
        {
            _projectileView.LookAt(targetPosition);
            _projectileView.SetSpeed(_projectileModel.Speed);
        }
    }
}