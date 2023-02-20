using Player;
using UI;
using UnityEngine;
using WayPoint;

namespace Gameplay.GameplayStates
{
    public class StartState : BaseGameplayState
    {
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private PlayerInputZone _playerInputZone;
        [SerializeField] private PlayerModel _playerPrefab;
        [SerializeField] private WayPointModel[] _wayPointModels;

        private PlayerModel _playerModel;
        private WayPointModel _startWayPoint;
        private int _currentGameProgress;
        private int _totalGameProgress;

        public override void Enter()
        {
            _startScreen.SetActive(true);
            _playerInputZone.SetActive(false);

            _startScreen.OnClick += StartGame;
            _playerInputZone.OnTouch += HandleTouchDown;

            _startWayPoint = _wayPointModels[0];
            _totalGameProgress = _wayPointModels.Length;

            _playerModel = Instantiate(_playerPrefab);

            _playerModel.SetStartPosition(_startWayPoint.PlayerDestination.position);
        }

        public override void Exit()
        {
            _startScreen.OnClick -= StartGame;
            _playerInputZone.OnTouch -= HandleTouchDown;

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
            _playerInputZone.SetActive(true);

            _startWayPoint.AddProgress();
        }

        private void HandleTouchDown(Vector3 touchPosition)
        {
            _playerModel.ShootProjectile(touchPosition);
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