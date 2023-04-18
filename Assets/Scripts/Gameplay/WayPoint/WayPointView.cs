using System;
using UnityEngine;

namespace WayPoint
{
    public class WayPointView : MonoBehaviour
    {
        public event Action<Collider> OnDestinationZoneEntered;

        private void OnTriggerEnter(Collider other)
        {
            OnDestinationZoneEntered?.Invoke(other);
        }
    }
}