using Level;
using Player;
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

        public override void Enter()
        {
            _startScreen.SetActive(true);
            _playerInputZone.SetActive(false);

            _startScreen.OnClick += StartGame;

            var playerModel = Instantiate(_playerPrefab);

            _levelModel.SetPlayerModel(playerModel);
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