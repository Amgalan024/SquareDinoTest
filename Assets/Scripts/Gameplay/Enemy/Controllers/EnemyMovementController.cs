using UnityEngine;
using WayPoint;

namespace Enemy
{
    public class EnemyMovementController : MonoBehaviour
    {
        [SerializeField] private EnemyModel _enemyModel;
        [SerializeField] private EnemyView _enemyView;
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
            _enemyView.MoveTo(_enemyModel.Target.position);
        }
    }
}