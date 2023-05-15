using System;
using UnityEngine;
using WayPoint;
using System.Linq;
using Level;
using UI;

namespace Player
{
    public class PlayerModel : MonoBehaviour
    {
        public event Action OnPlayerDied;

        [SerializeField] private Transform _cameraAnchor;
        [SerializeField] private float _rotationSpeed;

        public Transform CameraAnchor => _cameraAnchor;
        public float RotationSpeed => _rotationSpeed;

        public LevelModel LevelModel { get; private set; }
        public PlayerInputZone PlayerInputZone { get; private set; }
        public Transform ProjectilesRoot { get; private set; }

        public Transform CurrentTarget { get; private set; }

        public bool IsAlive { get; private set; }

        public void Initialize(LevelModel levelModel, PlayerInputZone playerInputZone, Transform projectilesRoot)
        {
            LevelModel = levelModel;
            PlayerInputZone = playerInputZone;
            ProjectilesRoot = projectilesRoot;
            IsAlive = true;
        }

        public void SetClosestTarget()
        {
            var nextTarget = LevelModel.CurrentWayPointGoals.FirstOrDefault(g => !g.IsAchieved);

            if (nextTarget != null)
            {
                CurrentTarget = nextTarget.transform;
            }
            else
            {
                CurrentTarget = null;
            }
        }

        public void KillPlayer()
        {
            if (IsAlive)
            {
                IsAlive = false;
                OnPlayerDied?.Invoke();
                Debug.Log("Player Killed");
            }
        }
    }
}