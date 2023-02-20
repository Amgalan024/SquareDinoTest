using Gameplay;
using Gameplay.GameplayStates;
using UnityEngine;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private GameplayStateMachine _gameplayStateMachine;

        [SerializeField] private LevelModel _levelModel;

        private void Awake()
        {
            _levelModel.OnLevelStarted += HandleLevelStart;
            _levelModel.OnLevelProgressed += HandleLevelProgress;
            _levelModel.OnLevelCompleted += _gameplayStateMachine.ChangeState<FinishState>;
            _levelModel.OnPlayerCreated += SetupPlayer;
        }

        private void OnDestroy()
        {
            _levelModel.OnLevelStarted -= HandleLevelStart;
            _levelModel.OnLevelProgressed -= HandleLevelProgress;
            _levelModel.OnLevelCompleted -= _gameplayStateMachine.ChangeState<FinishState>;
            _levelModel.OnPlayerCreated -= SetupPlayer;

            foreach (var wayPointModel in _levelModel.WayPointModels)
            {
                wayPointModel.OnWayPointPassed -= _levelModel.AddProgress;
            }
        }

        private void SetupPlayer()
        {
            _levelModel.PlayerModel.ProjectilesRoot = _levelModel.ProjectilesRoot;

            _levelModel.PlayerModel.SetStartPosition(_levelModel.StartWayPoint.PlayerDestination.position);
        }

        private void HandleLevelProgress(int currentProgress)
        {
            _levelModel.PlayerModel.SetCurrentWayPointGoals(_levelModel.WayPointModels[currentProgress]
                .WayPointGoals);

            _levelModel.PlayerModel.SetDestination(_levelModel.WayPointModels[currentProgress].PlayerDestination
                .position);
        }

        private void HandleLevelStart()
        {
            foreach (var wayPointModel in _levelModel.WayPointModels)
            {
                wayPointModel.OnWayPointPassed += _levelModel.AddProgress;
            }

            _levelModel.StartWayPoint.AddProgress();

            _levelModel.PlayerModel.SetStartPosition(_levelModel.StartWayPoint.PlayerDestination.position);
        }
    }
}