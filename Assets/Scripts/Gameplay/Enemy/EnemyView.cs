using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class BodyPart
    {
        public Transform Transform;
        public Vector3 LocalPosition;
        public Quaternion LocalRotation;
    }

    public class EnemyView : MonoBehaviour
    {
        private static readonly int Velocity = Animator.StringToHash("Velocity");

        public event Action<EnemyView, Collider> OnHit;

        [SerializeField] private Animator _animator;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public HealthBar HealthBar => _healthBar;

        private RagDollPartView[] _ragDollParts;
        private Rigidbody[] _ragDollRigidbodies;

        private List<BodyPart> _bodyParts = new List<BodyPart>();

        private void Start()
        {
            _ragDollParts = GetComponentsInChildren<RagDollPartView>();
            _ragDollRigidbodies = GetComponentsInChildren<Rigidbody>();

            foreach (var bodyPart in _ragDollParts)
            {
                bodyPart.OnHit += OnBodyPartHit;
            }

            _animator.enabled = true;

            SetRagDollRigidBodiesKinematic(true);

            foreach (var bodyBone in (HumanBodyBones[]) Enum.GetValues(typeof(HumanBodyBones)))
            {
                if (bodyBone == HumanBodyBones.LastBone)
                {
                    return;
                }

                var part = _animator.GetBoneTransform(bodyBone);
                if (part != null)
                {
                    var bodyPart = new BodyPart
                    {
                        Transform = part,
                        LocalPosition = Vector3.zero,
                        LocalRotation = Quaternion.identity
                    };

                    _bodyParts.Add(bodyPart);
                }
            }
        }

        private void Update()
        {
            _animator.SetFloat(Velocity, _navMeshAgent.velocity.magnitude);
        }

        private void OnDestroy()
        {
            foreach (var bodyPart in _ragDollParts)
            {
                bodyPart.OnHit -= OnBodyPartHit;
            }
        }

        public void MoveTo(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
        }

        public void StopMovement()
        {
            _navMeshAgent.isStopped = true;
        }

        public void ResumeMovement()
        {
            _navMeshAgent.isStopped = false;
        }

        public void PlayDeathAnimation()
        {
            FromAnimationToRagDoll();

            _animator.enabled = false;
            StopMovement();
            SetRagDollRigidBodiesKinematic(false);
        }

        public async UniTask PlayReviveAnimationAsync(float reviveDuration)
        {
            SetRagDollRigidBodiesKinematic(true);

            var sequence = DOTween.Sequence();

            foreach (var bodyPart in _bodyParts)
            {
                sequence.Join(bodyPart.Transform.DOLocalMove(bodyPart.LocalPosition, reviveDuration));
                sequence.Join(bodyPart.Transform.DOLocalRotate(bodyPart.LocalRotation.eulerAngles, reviveDuration));
            }

            await sequence.AwaitForComplete().ContinueWith(() =>
            {
                _animator.enabled = true;
                ResumeMovement();
            });
        }

        public void SetHealthBarValue(int currentHealth)
        {
            _healthBar.SetValue(currentHealth);
        }

        private void FromAnimationToRagDoll()
        {
            foreach (var bodyPart in _bodyParts)
            {
                bodyPart.LocalPosition = bodyPart.Transform.localPosition;
                bodyPart.LocalRotation = bodyPart.Transform.localRotation;
            }
        }

        private void OnBodyPartHit(Collider otherCollider)
        {
            OnHit?.Invoke(this, otherCollider);
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