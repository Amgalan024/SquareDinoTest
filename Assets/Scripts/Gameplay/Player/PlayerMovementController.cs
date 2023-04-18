using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private PlayerView _playerView;

        private void Start()
        {
            _playerModel.LevelModel.OnLevelStarted += HandleLevelStart;
            _playerModel.LevelModel.OnLevelProgressed += HandleLevelProgress;
            _playerModel.LevelModel.OnLevelCompleted += HandleLevelCompletion;

            _playerView.SetPosition(_playerModel.LevelModel.StartWayPoint.PlayerDestination.position);

            foreach (var wayPointModel in _playerModel.LevelModel.WayPointModels)
            {
                wayPointModel.OnPlayerArrived += _playerModel.SetClosestTarget;

                foreach (var wayPointGoal in wayPointModel.WayPointGoals)
                {
                    wayPointGoal.OnGoalAchieved += _playerModel.SetClosestTarget;
                }
            }
        }

        private void HandleLevelStart()
        {
            HandleLevelProgress(0);
        }

        private void Update()
        {
            RotateTowardsTarget();
        }

        private void OnDestroy()
        {
            _playerModel.LevelModel.OnLevelStarted -= HandleLevelStart;
            _playerModel.LevelModel.OnLevelProgressed -= HandleLevelProgress;
            _playerModel.LevelModel.OnLevelCompleted -= HandleLevelCompletion;

            foreach (var wayPointModel in _playerModel.LevelModel.WayPointModels)
            {
                wayPointModel.OnPlayerArrived -= _playerModel.SetClosestTarget;

                foreach (var wayPointGoal in wayPointModel.WayPointGoals)
                {
                    wayPointGoal.OnGoalAchieved -= _playerModel.SetClosestTarget;
                }
            }
        }

        private void HandleLevelProgress(int currentProgress)
        {
            _playerView.MoveTo(_playerModel.LevelModel.WayPointModels[currentProgress].PlayerDestination.position);
        }

        private void HandleLevelCompletion()
        {
            _playerView.MoveTo(_playerModel.LevelModel.FinishWayPoint.PlayerDestination.transform.position);
        }

        private void RotateTowardsTarget()
        {
            if (_playerModel.CurrentTarget == null)
            {
                return;
            }

            var direction = (_playerModel.CurrentTarget.position - transform.position).normalized;

            var lookRotation = Quaternion.LookRotation(direction);

            var lookRotationEuler = lookRotation.eulerAngles;

            var playerEuler = transform.eulerAngles;

            lookRotation = Quaternion.Euler(new Vector3(playerEuler.x, lookRotationEuler.y, playerEuler.z));

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,
                Time.deltaTime * _playerModel.RotationSpeed);
        }
    }
}