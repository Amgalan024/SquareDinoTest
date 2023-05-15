using System;
using UnityEngine;

namespace WayPoint
{
    public class WayPointView : MonoBehaviour
    {
        public event Action<Collider> OnDestinationZoneEntered;

        [SerializeField] private Collider _destinationCollider;

        public Collider DestinationCollider => _destinationCollider;

        private void OnTriggerEnter(Collider other)
        {
            OnDestinationZoneEntered?.Invoke(other);
        }
    }
}