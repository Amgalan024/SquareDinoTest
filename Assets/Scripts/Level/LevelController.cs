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

            _levelModel.FinishWayPoint.OnPlayerArrived += HandlePlayerArrivalAtFinish;
        }

        private void OnDestroy()
        {
            _levelModel.OnLevelStarted -= HandleLevelStart;

            _levelModel.FinishWayPoint.OnPlayerArrived -= HandlePlayerArrivalAtFinish;

            foreach (var wayPointModel in _levelModel.WayPointModels)
            {
                wayPointModel.OnWayPointPassed -= _levelModel.AddProgress;
            }
        }

        private void HandleLevelStart()
        {
            foreach (var wayPointModel in _levelModel.WayPointModels)
            {
                wayPointModel.OnWayPointPassed += _levelModel.AddProgress;
            }
        }

        private void HandlePlayerArrivalAtFinish()
        {
            _gameplayStateMachine.ChangeState<FinishState>();
        }
    }
}