using System.Linq;
using Gameplay.GameplayStates;
using UnityEngine;

namespace Gameplay
{
    public class GameplayStateMachine : MonoBehaviour
    {
        [SerializeField] private BaseGameplayState[] _gameplayStates;

        private BaseGameplayState _currentState;

        public void Initialize()
        {
            foreach (var gameplayState in _gameplayStates)
            {
                gameplayState.SetStateMachine(this);
            }
        }

        public void ChangeState<T>() where T : BaseGameplayState
        {
            var newState = _gameplayStates.First(s => s.GetType() == typeof(T));

            ChangeState(newState);
        }

        private void ChangeState(BaseGameplayState newState)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = newState;

            _currentState.Enter();
        }
    }
}