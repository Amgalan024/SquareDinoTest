using Gameplay;
using Gameplay.GameplayStates;
using UnityEngine;

namespace Startup
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private StateMachine _levelStateMachine;

        private void Start()
        {
            _levelStateMachine.Initialize();

            _levelStateMachine.ChangeState<StartState>();
        }
    }
}