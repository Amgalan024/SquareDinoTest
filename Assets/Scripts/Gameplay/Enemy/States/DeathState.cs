using Gameplay.GameplayStates;
using UnityEngine;
using WayPoint;

namespace Enemy.States
{
    public class DeathState : BaseState
    {
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private WayPointGoalModel _wayPointGoalModel;

        public override void Enter()
        {
            _enemyView.HealthBar.SetActive(false);

            _wayPointGoalModel.AchieveGoal();
        }
    }
}