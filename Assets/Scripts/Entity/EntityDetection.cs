using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class EntityDetection : MonoBehaviour
    {
        [Header("Detection Parameters")] public string targetTag = "Player";
        public List<Collider2D> detectedColliders = new();

        // When gets in range
        private void OnTriggerEnter2D(Collider2D pCol)
        {
            Debug.Log("EnterTrigger");
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