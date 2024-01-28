using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public float swordDamage = 1f;
    public float knockbackForce = 1000f;

    public Collider2D hitbox;

    private void Start()
    {
        if (hitbox == null)
        {
            Debug.LogError("Sword Collider is not set");
        }
    }

    // Vérifie la présence du rigibody d'un ennemi et envoie les dommages au GameObject
    private void OnTriggerEnter2D(Collider2D col)
    {
        var damageableObject = col.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            // Calcule la direction entre le personnage et la cible
            var parentPosition = transform.parent.position;

            var direction = (Vector2)(col.transform.position - parentPosition).normalized;
            var knockback = direction * knockbackForce;

            damageableObject.OnHit(swordDamage, knockback);
        }
        else
        {
            Debug.LogWarning("Collider does not implement IDamageable");
        }
    }

    private void IsFacingRight(bool isFacingRight)
    {
        var go = gameObject;
        var pos = go.transform.localPosition;
        var scale = go.transform.localScale;
        if (isFacingRight)
        {
            pos.x = 1;
            scale.x = 1;
        }
        else
        {
            pos.x = -1;
            scale.x = -1;
        }

        go.transform.localPosition = pos;
        go.transform.localScale = scale;
    }
}