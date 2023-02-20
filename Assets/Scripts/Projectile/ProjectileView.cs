using System;
using UnityEngine;

namespace Player
{
    public class ProjectileView : MonoBehaviour
    {
        public event Action<ProjectileView, Collision> OnHit;

        [SerializeField] private Rigidbody _rigidbody;

        private void OnCollisionEnter(Collision collision)
        {
            OnHit?.Invoke(this, collision);
        }

        public void Initialize()
        {
            gameObject.SetActive(true);
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }

        public void SetTarget(Vector3 direction)
        {
            transform.LookAt(direction);
        }

        public void SetSpeed(float speed)
        {
            _rigidbody.velocity = transform.forward * speed;
        }
    }
}