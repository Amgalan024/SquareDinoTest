using System;
using UnityEngine;
using WayPoint;
using System.Linq;

namespace Player
{
    public class PlayerModel : MonoBehaviour
    {
        public event Action<Vector3> OnDestinationSet;
        public event Action<Vector3> OnStartPositionSet;
        public event Action<Vector3> OnShootCommand;

        public event Action OnGoalsRefreshed;

        [SerializeField] private float _rotationSpeed;
        public float RotationSpeed => _rotationSpeed;

        public Transform ProjectilesRoot { get; set; }
        public Transform CurrentTarget { get; private set; }
        public WayPointGoalModel[] CurrentWayPointGoals { get; private set; }

        public void SetDestination(Vector3 destination)
        {
            OnDestinationSet?.Invoke(destination);
        }

        public void SetStartPosition(Vector3 startPosition)
        {
            OnStartPositionSet?.Invoke(startPosition);
        }

        public void ShootProjectile(Vector3 touchPosition)
        {
            OnShootCommand?.Invoke(touchPosition);
        }

        public void SetCurrentWayPointGoals(WayPointGoalModel[] wayPointGoalModels)
        {
            CurrentWayPointGoals = wayPointGoalModels
                .OrderBy(w => (w.transform.position - transform.position).magnitude).ToArray();

            OnGoalsRefreshed?.Invoke();
        }

        public void SetClosestTarget()
        {
            var nextTarget = CurrentWayPointGoals.FirstOrDefault(g => !g.IsAchieved);

            if (nextTarget != null)
            {
                CurrentTarget = nextTarget.transform;
            }
        }
    }
}