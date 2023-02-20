using System.Collections.Generic;
using UnityEngine;
using WayPoint;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerProjectileShooter _playerProjectileShooter;
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private Transform _cameraAnchor;

        private Camera _camera;

        private List<WayPointGoalModel> _subscriptions = new List<WayPointGoalModel>();

        private void Start()
        {
            _playerModel.OnGoalsRefreshed += OnGoalsRefreshed;
            _playerModel.OnStartPositionSet += _playerView.SetPosition;
            _playerModel.OnDestinationSet += _playerView.MoveTo;

            _camera = Camera.main;
        }

        private void OnDestroy()
        {
            _playerModel.OnGoalsRefreshed -= OnGoalsRefreshed;
            _playerModel.OnStartPositionSet -= _playerView.SetPosition;
            _playerModel.OnDestinationSet -= _playerView.MoveTo;

            foreach (var subscription in _subscriptions)
            {
                subscription.OnGoalAchieved -= _playerModel.SetNewTarget;
            }
        }

        private void Update()
        {
            HandlePlayerAim();
            HandleCameraPosition();
            HandleTouchInput();
        }

        private void OnGoalsRefreshed()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription.OnGoalAchieved -= _playerModel.SetNewTarget;
            }

            _subscriptions.Clear();

            _playerModel.SetNewTarget();

            foreach (var wayPointGoal in _playerModel.CurrentWayPointGoals)
            {
                wayPointGoal.OnGoalAchieved += _playerModel.SetNewTarget;
            }

            _subscriptions.AddRange(_playerModel.CurrentWayPointGoals);
        }

        private void HandlePlayerAim()
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

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
        }

        private void HandleCameraPosition()
        {
            _camera.transform.position = _cameraAnchor.position;

            Vector3 cameraEulerAngles = _camera.transform.rotation.eulerAngles;

            Vector3 newCameraAngle =
                new Vector3(cameraEulerAngles.x, transform.rotation.eulerAngles.y, cameraEulerAngles.z);

            _camera.transform.rotation = Quaternion.Euler(newCameraAngle);
        }

        private void HandleTouchInput()
        {
            if (!_playerModel.IsInputEnabled)
            {
                return;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _playerProjectileShooter.ShootProjectile(touch.position);
                }
            }
        }
    }
}