using UnityEngine;

namespace Gameplay.GameplayStates
{
    public abstract class BaseState : MonoBehaviour
    {
        protected StateMachine StateMachine;

        public void SetStateMachine(StateMachine stateMachine)
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