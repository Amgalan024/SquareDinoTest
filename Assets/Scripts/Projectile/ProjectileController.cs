using UnityEngine;

namespace Player
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private ProjectileModel _projectileModel;
        [SerializeField] private ProjectileView _projectileView;

        private void Start()
        {
            _projectileView.SetSpeed(_projectileModel.Speed);

            _projectileView.OnHit += HandleProjectileHit;
        }

        private void OnDestroy()
        {
            _projectileView.OnHit -= HandleProjectileHit;
        }

        private void HandleProjectileHit(ProjectileView projectileView, Collision collision)
        {
            projectileView.ReturnToPool();
        }
    }
}