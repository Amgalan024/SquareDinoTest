using Player;
using UnityEngine;

namespace WayPoint
{
    public class WayPointController : MonoBehaviour
    {
        [SerializeField] private WayPointModel _wayPointModel;
        [SerializeField] private WayPointView _wayPointView;
        
        private void Awake()
        {
            foreach (var wayPointGoal in _wayPointModel.WayPointGoals)
            {
                wayPointGoal.OnGoalAchieved += _wayPointModel.AddProgress;
            }
            
            _wayPointView.OnDestinationZoneEntered += HandleDestinationZoneEnter;
        }

        private void HandleDestinationZoneEnter(Collider otherCollider)
        {
            if (otherCollider.attachedRigidbody.GetComponent<PlayerModel>())
            {
                _wayPointModel.InvokePlayerArriving();
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