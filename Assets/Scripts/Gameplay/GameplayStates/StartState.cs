using Player;
using UI;
using UnityEngine;
using WayPoint;

namespace Gameplay.GameplayStates
{
    public class StartState : BaseGameplayState
    {
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private WayPointModel[] _wayPointModels;

        private WayPointModel _startWayPoint;
        private int _currentGameProgress;
        private int _totalGameProgress;

        public override void Enter()
        {
            _startScreen.SetActive(true);

            _startScreen.OnClick += StartGame;

            _startWayPoint = _wayPointModels[0];
            _totalGameProgress = _wayPointModels.Length;

            _playerModel.SetStartPosition(_startWayPoint.PlayerDestination.position);
        }

        public override void Exit()
        {
            _startScreen.OnClick -= StartGame;

            foreach (var wayPointController in _wayPointModels)
            {
                wayPointController.OnWayPointPassed -= AddProgress;
            }
        }

        private void StartGame()
        {
            foreach (var wayPointController in _wayPointModels)
            {
                wayPointController.OnWayPointPassed += AddProgress;
            }

            _startScreen.SetActive(false);

            _playerModel.IsInputEnabled = true;

            _startWayPoint.AddProgress();
        }

        private void AddProgress()
        {
            _currentGameProgress++;

            if (_currentGameProgress >= _totalGameProgress)
            {
                StateMachine.ChangeState<FinishState>();
            }
            else
            {
                _playerModel.SetCurrentWayPointGoals(_wayPointModels[_currentGameProgress].WayPointGoals);

                _playerModel.SetDestination(_wayPointModels[_currentGameProgress].PlayerDestination.position);
            }
        }
    }
}