using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public float weaponDamage = 1f;
    public float knockbackForce = 15f;

    public Collider2D hitbox;

    private void Start()
    {
        if (hitbox == null)
        {
            Debug.LogError("Weapon Collider is not set");
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

            damageableObject.OnHit(weaponDamage, knockback);
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