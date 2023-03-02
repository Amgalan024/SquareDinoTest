using Player;
using UnityEngine;

namespace UI
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private PlayerProjectileShooter _playerProjectileShooter;
        [SerializeField] private PlayerModel _playerModel;

        private void Start()
        {
            _playerModel.PlayerInputZone.OnTouch += Shoot;
        }

        private void OnDestroy()
        {
            _playerModel.PlayerInputZone.OnTouch -= Shoot;
        }

        private void Shoot(Vector3 touchPosition)
        {
            _playerProjectileShooter.ShootProjectile(touchPosition);
        }
    }
}