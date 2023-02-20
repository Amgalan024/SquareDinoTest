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

        public void SetValue(float value)
        {
            _healthBar.value = value;
        }
    }
}