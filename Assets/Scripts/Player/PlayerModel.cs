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
        public event Action OnGoalsRefreshed;

        public bool IsInputEnabled { get; set; }

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

        public void SetCurrentWayPointGoals(WayPointGoalModel[] wayPointGoalModels)
        {
            CurrentWayPointGoals = wayPointGoalModels;

            OnGoalsRefreshed?.Invoke();
        }

        public void SetNewTarget()
        {
            var nextTarget = CurrentWayPointGoals.FirstOrDefault(g => !g.IsAchieved);

            if (nextTarget != null)
            {
                CurrentTarget = nextTarget.transform;
            }
        }
    }
}