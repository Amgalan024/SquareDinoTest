using Gameplay;
using Gameplay.GameplayStates;
using UnityEngine;

namespace Startup
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private GameplayStateMachine _gameplayStateMachine;

        private void Start()
        {
            _gameplayStateMachine.Initialize();

            _gameplayStateMachine.ChangeState<StartState>();
        }
    }
}