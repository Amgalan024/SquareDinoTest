using UnityEngine;

namespace Projectile.Behaviours
{
    public class KnifeBehaviour : MonoBehaviour
    {
        [SerializeField] private float _penetration;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        private void OnCollisionEnter(Collision collision)
        {
            _collider.enabled = false;

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.isKinematic = true;

            var penetrationVector = transform.forward.normalized * _penetration;
            transform.position = transform.position + penetrationVector;

            transform.SetParent(collision.transform);
        }
    }
}