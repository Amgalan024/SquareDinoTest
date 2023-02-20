using UnityEngine;

namespace Player
{
    public class ProjectileModel : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;

        public float Speed => _speed;
        public int Damage => _damage;
    }
}