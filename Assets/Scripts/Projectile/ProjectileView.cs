﻿using System;
using UnityEngine;

namespace Player
{
    public class ProjectileView : MonoBehaviour
    {
        public event Action<ProjectileView, Collision> OnHit;

        [SerializeField] private Rigidbody _rigidbody;

        private void OnCollisionEnter(Collision collision)
        {
            OnHit?.Invoke(this, collision);
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void LookAt(Vector3 position)
        {
            transform.LookAt(position);
        }

        public void SetSpeed(float speed)
        {
            _rigidbody.velocity = transform.forward * speed;
        }
    }
}