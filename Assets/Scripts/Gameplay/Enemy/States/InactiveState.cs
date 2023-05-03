using Gameplay.GameplayStates;
using UnityEngine;

namespace Enemy.States
{
    public class InactiveState : BaseState
    {
        [SerializeField] private EnemyView _enemyView;

        public override void Enter()
        {
            _enemyView.StopMovement();
        }
    }
}