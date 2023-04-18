using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _healthBar;

        public void Initialize(float value)
        {
            _healthBar.maxValue = value;
            _healthBar.minValue = 0;
            _healthBar.value = value;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void LookAt(Vector3 position)
        {
            transform.LookAt(position);
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetValue(float value)
        {
            _healthBar.value = value;
        }
    }
}