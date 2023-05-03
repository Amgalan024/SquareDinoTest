using System.Collections.Generic;
using Enemy;
using Level;
using Player;
using Scripts.Camera;
using UI;
using UnityEngine;

namespace Gameplay.GameplayStates
{
    public class StartState : BaseState
    {
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private PlayerInputZone _playerInputZone;
        [SerializeField] private PlayerModel _playerPrefab;
        [SerializeField] private LevelModel _levelModel;
        [SerializeField] private CameraModel _cameraModel;
        [SerializeField] private Transform _projectilesRoot;

        private PlayerModel _playerModel;

        public override void Enter()
        {
            SetupStartTrigger();

            InitializePlayer();
            InitializeCamera();
            InitializeEnemies();
            InitializeWayPoints();
        }

        public override void Exit()
        {
            _startScreen.OnClick -= StartLevel;
            _levelModel.FinishWayPoint.OnPlayerArrived -= FinishLevel;
        }

        private void SetupStartTrigger()
        {
            _startScreen.SetActive(true);

            _startScreen.OnClick += StartLevel;

            _playerInputZone.SetActive(false);
        }

        private void StartLevel()
        {
            _startScreen.SetActive(false);
            _playerInputZone.SetActive(true);

            _levelModel.StartLevel();
        }

        private void RestartLevel()
        {
            StateMachine.ChangeState<RestartState>();
        }

        private void FinishLevel()
        {
            StateMachine.ChangeState<FinishState>();
        }

        private void InitializePlayer()
        {
            _playerModel = Instantiate(_playerPrefab, _levelModel.StartWayPoint.PlayerDestination.position,
                Quaternion.identity);

            _playerModel.Initialize(_levelModel, _playerInputZone, _projectilesRoot);

            _playerModel.OnPlayerDied += RestartLevel;
        }

        private void InitializeCamera()
        {
            _cameraModel.Anchor = _playerModel.CameraAnchor;
        }

        private void InitializeWayPoints()
        {
            foreach (var wayPointModel in _levelModel.WayPointModels)
            {
                wayPointModel.OnWayPointPassed += _levelModel.AddProgress;
            }

            _levelModel.FinishWayPoint.OnPlayerArrived += FinishLevel;
        }

        private void InitializeEnemies()
        {
            var enemies = new List<EnemyModel>();

            foreach (var wayPointModel in _levelModel.WayPointModels)
            {
                foreach (var wayPointGoal in wayPointModel.WayPointGoals)
                {
                    if (wayPointGoal.TryGetComponent(out EnemyModel enemyModel))
                    {
                        enemies.Add(enemyModel);
                    }
                }
            }

            foreach (var enemy in enemies)
            {
                enemy.Initialize(_playerModel.transform);
            }
        }
    }
}