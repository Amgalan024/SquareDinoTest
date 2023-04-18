using System;
using UnityEngine;

namespace Enemy
{
    public class RagDollPartView : MonoBehaviour
    {
        public event Action<Collider> OnHit;

        private void OnTriggerEnter(Collider otherCollider)
        {
            OnHit?.Invoke(otherCollider);
        }
    }
}