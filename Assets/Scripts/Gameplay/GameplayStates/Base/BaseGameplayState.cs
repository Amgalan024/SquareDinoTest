using UnityEngine;

namespace Gameplay.GameplayStates
{
    public abstract class BaseGameplayState : MonoBehaviour
    {
        protected GameplayStateMachine StateMachine;

        public void SetStateMachine(GameplayStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}