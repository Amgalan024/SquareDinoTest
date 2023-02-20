using System;
using UnityEngine;

namespace Player
{
    public class PlayerModel : MonoBehaviour
    {
        public event Action<Vector3> OnDestinationSet;
        public event Action<Vector3> OnStartPositionSet;
        
        public bool IsInputEnabled { get; set; }

        public void SetDestination(Vector3 destination)
        {
            OnDestinationSet?.Invoke(destination);
        }

        public void SetStartPosition(Vector3 startPosition)
        {
            OnStartPositionSet?.Invoke(startPosition);
        }
    }
}