using System;
using Player;
using UnityEngine;
using WayPoint;

namespace Level
{
    public class LevelModel : MonoBehaviour
    {
        public event Action OnLevelCompleted;
        public event Action OnLevelStarted;
        public event Action<int> OnLevelProgressed;

        public event Action OnPlayerCreated;

        [SerializeField] private WayPointModel _startWayPoint;
        [SerializeField] private WayPointModel[] _wayPointModels;
        [SerializeField] private WayPointModel _finishWayPoint;
        [SerializeField] private Transform _projectilesRoot;

        public WayPointModel StartWayPoint => _startWayPoint;
        public WayPointModel[] WayPointModels => _wayPointModels;
        public WayPointModel FinishWayPoint => _finishWayPoint;
        public Transform ProjectilesRoot => _projectilesRoot;
        public PlayerModel PlayerModel { get; private set; }

        private int _currentGameProgress;
        private int _totalGameProgress;

        private void Awake()
        {
            _totalGameProgress = _wayPointModels.Length;
        }

        public void SetPlayerModel(PlayerModel playerModel)
        {
            PlayerModel = playerModel;

            OnPlayerCreated?.Invoke();
        }

        public void StartLevel()
        {
            OnLevelStarted?.Invoke();
        }

        public void AddProgress()
        {
            _currentGameProgress++;

            if (_currentGameProgress >= _totalGameProgress)
            {
                OnLevelCompleted?.Invoke();
            }
            else
            {
                OnLevelProgressed?.Invoke(_currentGameProgress);
            }
        }
    }
}