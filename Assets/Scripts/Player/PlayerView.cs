using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        private static readonly int Velocity = Animator.StringToHash("Velocity");

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;

        private void Update()
        {
            _animator.SetFloat(Velocity, _navMeshAgent.velocity.magnitude);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void MoveTo(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
        }
    }
}