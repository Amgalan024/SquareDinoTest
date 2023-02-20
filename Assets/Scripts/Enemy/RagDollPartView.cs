using System;
using UnityEngine;

namespace Enemy
{
    public class RagDollPartView : MonoBehaviour
    {
        public event Action<Collision> OnHit;

        private void OnCollisionEnter(Collision collision)
        {
            OnHit?.Invoke(collision);
        }
    }
}