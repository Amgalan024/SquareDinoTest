using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class PlayerInputZone : MonoBehaviour, IPointerDownHandler
    {
        public event Action<Vector3> OnTouch;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnTouch?.Invoke(eventData.position);
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}