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
            _levelModel.OnLevelCompleted += HandleLevelCompletion;
            _levelModel.OnPlayerCreated += SetupPlayer;

            _levelModel.FinishWayPoint.OnPlayerArrived += HandlePlayerArrivalAtFinish;
        }

        private void OnDestroy()
        {
            _levelModel.OnLevelStarted -= HandleLevelStart;
            _levelModel.OnLevelProgressed -= HandleLevelProgress;
            _levelModel.OnLevelCompleted -= HandleLevelCompletion;
            _levelModel.OnPlayerCreated -= SetupPlayer;

            _levelModel.FinishWayPoint.OnPlayerArrived -= HandlePlayerArrivalAtFinish;

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

        private void HandleLevelStart()
        {
            foreach (var wayPointModel in _levelModel.WayPointModels)
            {
                wayPointModel.OnWayPointPassed += _levelModel.AddProgress;
            }

            HandleLevelProgress(0);

            _levelModel.PlayerModel.SetStartPosition(_levelModel.StartWayPoint.PlayerDestination.position);
        }

        private void HandleLevelProgress(int currentProgress)
        {
            _levelModel.PlayerModel.SetCurrentWayPointGoals(_levelModel.WayPointModels[currentProgress]
                .WayPointGoals);

            _levelModel.PlayerModel.SetDestination(_levelModel.WayPointModels[currentProgress].PlayerDestination
                .position);
        }

        private void HandleLevelCompletion()
        {
            _levelModel.PlayerModel.SetDestination(_levelModel.FinishWayPoint.PlayerDestination.transform.position);
        }

        private void HandlePlayerArrivalAtFinish()
        {
            _gameplayStateMachine.ChangeState<FinishState>();
        }
    }
}