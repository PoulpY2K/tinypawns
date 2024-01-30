using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Mine
{
    public class GoldMine : MonoBehaviour
    {
        public EntityDetection detectionZone;

        private SpriteRenderer _sr;
        
        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
        }
        
        private void FixedUpdate()
        {
            if (detectionZone.detectedColliders.Count > 0)
            {
                var detectedObject = detectionZone.detectedColliders[0];
                Debug.Log(detectedObject.name);
            }
        }
    }
}