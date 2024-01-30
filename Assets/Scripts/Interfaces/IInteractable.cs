using UnityEngine;

namespace Interfaces
{
    public interface IInteractable
    {
        public void Reset();
        public void Interact();
        public void OnTriggerEnter2D(Collider2D col);
        public void OnTriggerExit2D(Collider2D col);
    }
}