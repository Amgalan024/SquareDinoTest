using Level;
using Player;
using Scripts.Camera;
using UI;
using UnityEngine;

namespace Gameplay.GameplayStates
{
    public class StartState : BaseGameplayState
    {
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private PlayerInputZone _playerInputZone;
        [SerializeField] private PlayerModel _playerPrefab;
        [SerializeField] private LevelModel _levelModel;
        [SerializeField] private CameraModel _cameraModel;
        [SerializeField] private Transform _projectilesRoot;

        public override void Enter()
        {
            _startScreen.SetActive(true);

            _playerInputZone.SetActive(false);

            _startScreen.OnClick += StartGame;

            var playerModel = Instantiate(_playerPrefab);

            playerModel.Initialize(_levelModel, _playerInputZone, _projectilesRoot);

            _cameraModel.Anchor = playerModel.CameraAnchor;
        }

        public override void Exit()
        {
            _startScreen.OnClick -= StartGame;
        }

        private void StartGame()
        {
            _startScreen.SetActive(false);
            _playerInputZone.SetActive(true);

            _levelModel.StartLevel();
        }
    }
}