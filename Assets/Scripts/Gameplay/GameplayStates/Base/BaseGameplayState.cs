using UnityEngine;

namespace Gameplay.GameplayStates
{
    public abstract class BaseGameplayState : MonoBehaviour
    {
        public GameplayStateMachine StateMachine { get; set; }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}