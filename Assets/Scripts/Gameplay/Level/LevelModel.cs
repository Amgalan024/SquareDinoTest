using System;
using UnityEngine;
using WayPoint;

namespace Level
{
    public class LevelModel : MonoBehaviour
    {
        public event Action OnLevelCompleted;
        public event Action OnLevelStarted;
        public event Action<int> OnLevelProgressed;

        [SerializeField] private WayPointModel _startWayPoint;
        [SerializeField] private WayPointModel[] _wayPointModels;
        [SerializeField] private WayPointModel _finishWayPoint;

        public WayPointModel StartWayPoint => _startWayPoint;
        public WayPointModel[] WayPointModels => _wayPointModels;
        public WayPointModel FinishWayPoint => _finishWayPoint;
        
        public WayPointGoalModel[] CurrentWayPointGoals => WayPointModels[_currentGameProgress].WayPointGoals;

        private int _currentGameProgress;
        private int _totalGameProgress;

        private void Awake()
        {
            _totalGameProgress = _wayPointModels.Length;
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