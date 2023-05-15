using Enemy;
using Player;
using UnityEngine;

namespace Gameplay.Player.Controllers
{
    public class PlayerInteractionsController : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private PlayerModel _playerModel;

        private void Start()
        {
            _playerView.OnHit += HandlePlayerHit;
        }

        private void OnDestroy()
        {
            _playerView.OnHit -= HandlePlayerHit;
        }

        private void HandlePlayerHit(Collider otherCollider)
        {
            if (otherCollider.GetComponent<RagDollPartView>())
            {
                _playerModel.KillPlayer();
            }
        }
    }
}