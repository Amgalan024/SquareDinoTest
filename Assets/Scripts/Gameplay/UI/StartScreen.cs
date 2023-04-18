using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class StartScreen : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnClick;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}