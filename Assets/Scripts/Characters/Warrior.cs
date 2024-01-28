using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Characters
{
    public class Warrior : MonoBehaviour
    {
        public float Health { get; set; }
        public bool Targetable { get; set; }
        
        public float health = 3;
        public bool targetable = true;
        public void OnHit(float damage, Vector2 knockback)
        {
            throw new System.NotImplementedException();
        }

        public void OnHit(float damage)
        {
            throw new System.NotImplementedException();
        }

        public void DestroySelf()
        {
            throw new System.NotImplementedException();
        }
    } 
}