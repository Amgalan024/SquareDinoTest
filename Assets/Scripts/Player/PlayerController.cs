using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerProjectileShooter _playerProjectileShooter;
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private PlayerView _playerView;

        private Camera _camera;
        private Vector3 _cameraOffset;

        private void Start()
        {
            _playerModel.OnStartPositionSet += _playerView.SetPosition;
            _playerModel.OnDestinationSet += _playerView.MoveTo;

            _camera = Camera.main;

            _cameraOffset = _camera.transform.position - transform.position;
        }

        private void OnDestroy()
        {
            _playerModel.OnStartPositionSet -= _playerView.SetPosition;
            _playerModel.OnDestinationSet -= _playerView.MoveTo;
        }

        private void Update()
        {
            HandleCameraPosition();
            HandleTouchInput();
        }

        private void HandleCameraPosition()
        {
            _camera.transform.position = transform.position + _cameraOffset;
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