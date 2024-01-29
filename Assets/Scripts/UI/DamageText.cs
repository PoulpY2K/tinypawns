using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DamageText : MonoBehaviour
    {
        [Header("Damage Text Parameters")]
        public float timeToLive = 1f;
        public float floatSpeed = 4f;
        public float timeElapsed = 0f;
    
        private Vector3 _floatDirection = new (0, 1, 0);
        private TextMeshProUGUI _tm;
        private RectTransform _rt;
        private Color _startingColor;
    
        private void Awake()
        {
            _tm = GetComponent<TextMeshProUGUI>();
            _rt = GetComponent<RectTransform>();
            _startingColor = _tm.color;
        }

        private void Update()
        {
            timeElapsed += Time.deltaTime;

            _rt.position += _floatDirection * (floatSpeed * Time.deltaTime);
            _tm.color = new Color(_startingColor.r, _startingColor.g, _startingColor.b, 1 - timeElapsed / timeToLive);
        
            if (timeElapsed > timeToLive)
            {
                Destroy(gameObject);
            }
        }
    }
}