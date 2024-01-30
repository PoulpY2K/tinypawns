using Interfaces;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        public virtual void Reset()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        public abstract void Interact();
        
        // When gets in range
        public virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerController>().ShowInteractIcon();
            }
        }
        
        // When goes out of range
        public virtual void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerController>().HideInteractIcon();
            }
        }
    }
}