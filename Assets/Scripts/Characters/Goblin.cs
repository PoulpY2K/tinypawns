using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Characters
{
    public class Goblin : MonoBehaviour, IDamageable
    {
        private Animator _animator;
        private Rigidbody2D _rb;
        private Collider2D _collider;

        public float Health
        {
            get => health;
            set
            {
                if (value < health)
                {
                    _animator.SetTrigger(Hit);
                }

                health = value;


                if (health <= 0)
                {
                    _animator.SetTrigger(Death);
                    Targetable = false;
                }
            }
        }

        public bool Targetable
        {
            get => targetable;
            set
            {
                targetable = value;
                //_rb.simulated = value;
                _collider.enabled = value;
            }
        }

        public float health = 3;
        public bool targetable = true;

        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Death = Animator.StringToHash("Death");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        public void OnHit(float damage, Vector2 knockback)
        {
            Health -= damage;

            // Appliquer la force à l'entité
            _rb.AddForce(knockback);
        }

        public void OnHit(float damage)
        {
            Health -= damage;
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}