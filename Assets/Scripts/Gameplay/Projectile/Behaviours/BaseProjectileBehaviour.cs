using UnityEngine;

namespace Projectile.Behaviours
{
    public abstract class BaseProjectileBehaviour : MonoBehaviour
    {
        public abstract void HandleProjectileHit(Collider otherCollider);
        public abstract void SetupBehaviour();
    }
}