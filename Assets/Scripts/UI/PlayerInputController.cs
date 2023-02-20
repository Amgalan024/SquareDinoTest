using Level;
using UnityEngine;

namespace UI
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private LevelModel _levelModel;
        [SerializeField] private PlayerInputZone _playerInputZone;

        private void Awake()
        {
            _playerInputZone.OnTouch += Shoot;
        }

        private void OnDestroy()
        {
            _playerInputZone.OnTouch -= Shoot;
        }

        private void Shoot(Vector3 touchPosition)
        {
            _levelModel.PlayerModel.ShootProjectile(touchPosition);
        }
    }
}