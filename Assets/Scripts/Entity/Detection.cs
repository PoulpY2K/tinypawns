using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(Collider2D))]
    public class Detection : MonoBehaviour
    {
        [Header("Detection Parameters")] public string targetTag = "Player";
        public List<Collider2D> detectedColliders = new();

        private void Reset()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
        
        // When gets in range
        private void OnTriggerEnter2D(Collider2D pCol)
        {
            if (pCol.gameObject.CompareTag(targetTag))
            {
                detectedColliders.Add(pCol);
            }
        }
        
        // When goes out of range
        private void OnTriggerExit2D(Collider2D pCol)
        {
            if (pCol.gameObject.CompareTag(targetTag))
            {
                detectedColliders.Remove(pCol);
            }
        }
    }
}