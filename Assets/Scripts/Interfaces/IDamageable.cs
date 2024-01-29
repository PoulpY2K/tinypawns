using UnityEngine;

namespace Interfaces
{
    // Interface des propriétés et méthodes pour la classe Damageable
    public interface IDamageable
    {
        public float Health { set; get; }
        public bool Targetable { set; get; }
        public bool Invicible { set; get; }
        public void OnHit(float damage, Vector2 knockback);
        public void OnHit(float damage);
        public void DestroySelf();
    }
}