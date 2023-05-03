using System.Linq;
using Gameplay.GameplayStates;
using UnityEngine;

namespace Gameplay
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private BaseState[] _gameplayStates;

        private BaseState _currentState;

        public void Initialize()
        {
            foreach (var gameplayState in _gameplayStates)
            {
                gameplayState.SetStateMachine(this);
            }
        }

        public void ChangeState<T>() where T : BaseState
        {
            var newState = _gameplayStates.First(s => s.GetType() == typeof(T));

            ChangeState(newState);
        }

        private void ChangeState(BaseState newState)
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