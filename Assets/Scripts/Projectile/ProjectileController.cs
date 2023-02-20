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
            _projectileView.OnHit += (view, collision) =>
            {
                _projectileModel.ReturnToPool();
            };
            
            _projectileModel.OnTargetSet += vector3 =>
            {
                _projectileView.LookAt(vector3);
                _projectileView.SetSpeed(_projectileModel.Speed);
            };

            _getFromPool = () => _projectileView.SetActive(true);
            _projectileModel.OnGotFromPool += _getFromPool;

            _returnToPool = () => _projectileView.SetActive(false);
            _projectileModel.OnReturnedToPool += _returnToPool;
        }

        private void OnDestroy()
        {
            _projectileModel.OnTargetSet -= _projectileView.LookAt;

            _projectileModel.OnGotFromPool -= _getFromPool;
            _projectileModel.OnReturnedToPool -= _returnToPool;
        }
    }
}