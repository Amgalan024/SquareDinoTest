using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        public event Action<EnemyView, Collision> OnHit;

        [SerializeField] private Animator _animator;

        private RagDollPartView[] _ragDollParts;
        private Rigidbody[] _ragDollRigidbodies;

        public void Start()
        {
            _ragDollParts = GetComponentsInChildren<RagDollPartView>();
            _ragDollRigidbodies = GetComponentsInChildren<Rigidbody>();

            foreach (var bodyPart in _ragDollParts)
            {
                bodyPart.OnHit += OnBodyPartHit;
            }

            _animator.enabled = true;

            SetRagDollRigidBodiesKinematic(true);
        }

        private void OnBodyPartHit(Collision collision)
        {
            OnHit?.Invoke(this, collision);
        }

        public void PlayDeathAnimation()
        {
            _animator.enabled = false;

            SetRagDollRigidBodiesKinematic(false);
        }

        private void SetRagDollRigidBodiesKinematic(bool value)
        {
            foreach (var ragDollRigidbody in _ragDollRigidbodies)
            {
                ragDollRigidbody.isKinematic = value;
            }
        }
    }
}