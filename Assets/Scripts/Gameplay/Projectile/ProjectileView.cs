using System;
using UnityEngine;

namespace Projectile
{
    public class ProjectileView : MonoBehaviour
    {
        public event Action<Collider> OnHit;

        private void OnTriggerEnter(Collider otherCollider)
        {
            OnHit?.Invoke(otherCollider);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void LookAt(Vector3 position)
        {
            transform.LookAt(position);
        }
    }
}