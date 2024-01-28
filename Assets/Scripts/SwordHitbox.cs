using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
   public float swordDamage = 1f;
   
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
      col.SendMessage("OnHit", swordDamage);
   }
}
