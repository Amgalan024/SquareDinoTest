using UnityEngine;

namespace WayPoint
{
    public class WayPointController : MonoBehaviour
    {
        [SerializeField] private WayPointModel _wayPointModel;

        private void Start()
        {
            foreach (var wayPointGoal in _wayPointModel.WayPointGoals)
            {
                wayPointGoal.OnGoalAchieved += _wayPointModel.AddProgress;
            }
        }

        private void OnDestroy()
        {
            foreach (var wayPointGoal in _wayPointModel.WayPointGoals)
            {
                wayPointGoal.OnGoalAchieved -= _wayPointModel.AddProgress;
            }
        }
    }
}