using System;
using System.Collections;
using System.Collections.Generic;
using Entity.Interfaces;
using UnityEngine;

namespace Entity.Enemies
{
    public class Goblin : MonoBehaviour
    {
        [Header("Goblin Parameters")] public float damage = 1f;
        public float knockbackForce = 10f;
        [Range(1f, 100f)] public float moveSpeed = 15f;
        public Detection detectionZone;

        private Rigidbody2D _rb;
        private Animator _animator;
        private SpriteRenderer _sr;
        private Damageable _dmg;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sr = GetComponent<SpriteRenderer>();
            _dmg = GetComponent<Damageable>();
        }

        private void FixedUpdate()
        {
            if (_dmg.Targetable && detectionZone.detectedColliders.Count > 0)
            {
                var detectedObject = detectionZone.detectedColliders[0];
                // Calcule de la direction
                Vector2 direction = (detectedObject.transform.position - transform.position).normalized;

                // Déplacement vers l'objet détecté
                _rb.AddForce(direction * moveSpeed);
                UpdateDirectionAnimation(direction);

                if (_rb.totalForce.magnitude > 0)
                {
                    _animator.SetBool(IsMoving, true);
                }
            }
            else
            {
                _animator.SetBool(IsMoving, false);
            }
        }

        private void UpdateDirectionAnimation(Vector2 direction)
        {
            _sr.flipX = direction.x switch
            {
                > 0 => false,
                < 0 => true,
                _ => _sr.flipX
            };
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var damageableObject = col.collider.GetComponent<IDamageable>();

            if (damageableObject != null && col.gameObject.CompareTag("Player"))
            {
                // Calcule la direction entre le personnage et la cible
                var direction = (Vector2)(col.collider.transform.position - transform.position).normalized;
                var knockback = direction * knockbackForce;

                damageableObject.OnHit(damage, knockback);
            }
        }
    }
}