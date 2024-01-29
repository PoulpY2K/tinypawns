using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Entity.Interfaces;
using TMPro;
using UnityEngine;

namespace Entity
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        private Animator _animator;
        private Rigidbody2D _rb;
        private Collider2D _collider;
        private float _invincibleTimerElapsed = 0f;

        [Header("Entity Parameters")] public float health = 3;
        public bool targetable = true;
        public bool disableSimulationOnDeath = false;
        public bool canTurnInvicible;
        public bool invincible;
        public float invicibilityTime = 0.5f;

        [Header("Text Reference")] public TextMeshProUGUI damageText;

        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Death = Animator.StringToHash("Death");

        public float Health
        {
            get => health;
            set
            {
                if (value < health)
                {
                    _animator.SetTrigger(Hit);

                    if (!gameObject.CompareTag("Resource"))
                    {
                        ShowDamageOnScreen(health - value);
                    }
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
                if (disableSimulationOnDeath)
                {
                    _rb.simulated = value;
                    _collider.enabled = value;
                }
            }
        }

        public bool Invicible
        {
            get => invincible;
            set
            {
                invincible = value;
                if (invincible)
                {
                    _invincibleTimerElapsed = 0f;
                }
            }
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        private void FixedUpdate()
        {
            if (!Invicible) return;

            _invincibleTimerElapsed += Time.deltaTime;

            if (_invincibleTimerElapsed > invicibilityTime)
            {
                Invicible = false;
            }
        }

        public void OnHit(float pDamage, Vector2 knockback)
        {
            if (Invicible) return;

            Health -= pDamage;

            // Appliquer la force à l'entité
            _rb.AddForce(knockback, ForceMode2D.Impulse);

            HandleInvincibility();
        }

        public void OnHit(float pDamage)
        {
            if (Invicible) return;

            Health -= pDamage;
            Debug.Log(Health);
            HandleInvincibility();
        }

        private void HandleInvincibility()
        {
            if (canTurnInvicible)
            {
                // Active l'invincibilité
                Invicible = true;
            }
        }

        private void ShowDamageOnScreen(float value)
        {
            var textRectTransform = Instantiate(damageText).GetComponent<RectTransform>();
            textRectTransform.gameObject.GetComponent<TextMeshProUGUI>().text = value.ToString();
            var pos = textRectTransform.transform.position;

            if (Camera.main)
            {
                pos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            }

            // Remonte le texte par rapport au pivot des personnages pour qu'il soit au dessus de leurs têtes
            pos.y += 100f;

            textRectTransform.transform.position = pos;
            var canvas = FindFirstObjectByType<Canvas>();
            textRectTransform.transform.SetParent(canvas.transform);
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}