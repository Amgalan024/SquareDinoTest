using System;
using UnityEngine;

namespace WayPoint
{
    public class WayPointModel : MonoBehaviour
    {
        public event Action OnWayPointPassed;
        public event Action OnPlayerArrived;

        [SerializeField] private WayPointGoalModel[] _wayPointGoals;
        [SerializeField] private Transform _playerDestination;

        public WayPointGoalModel[] WayPointGoals => _wayPointGoals;
        public Transform PlayerDestination => _playerDestination;

        private int _totalProgress;
        private int _currentProgress;

        private void Start()
        {
            _totalProgress = _wayPointGoals.Length;
        }

        public void AddProgress()
        {
            _currentProgress++;

            if (_currentProgress >= _totalProgress)
            {
                OnWayPointPassed?.Invoke();
            }
        }

        public void InvokePlayerArriving()
        {
            OnPlayerArrived?.Invoke();
        }
    }
}