using Enemy.States;
using Gameplay;
using UnityEngine;
using WayPoint;

namespace Enemy
{
    public class EnemyStateMachineController : MonoBehaviour
    {
        [SerializeField] private StateMachine _enemyStateMachine;
        [SerializeField] private WayPointGoalModel _wayPointGoalModel;

        private void Start()
        {
            _wayPointGoalModel.OnGoalActivated += HandleGoalActivation;
        }

        private void OnDestroy()
        {
            _wayPointGoalModel.OnGoalActivated -= HandleGoalActivation;
        }

        private void HandleGoalActivation()
        {
            _enemyStateMachine.ChangeState<ActiveState>();
        }
    }
}