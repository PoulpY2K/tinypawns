using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using Interfaces;
using Map.Interfaces;
using UnityEngine;

namespace Map
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Structure : MonoBehaviour, IInteractable, IInteractableStructure
    {
        public bool Activated { get; set; }
        public bool Destroyed { get; set; }

        public Sprite destroyed;
        public Sprite active;
        public Sprite inactive;

        protected SpriteRenderer _sr;

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _sr.sprite = inactive;
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public void Interact()
        {
            if (Destroyed) return;

            if (Activated)
            {
                _sr.sprite = inactive;
            }
            else
            {
                _sr.sprite = active;
            }

            Activated = !Activated;
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player") && !Destroyed)
            {
                col.GetComponent<PlayerController>().ShowInteractIcon();
            }
        }

        public void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerController>().HideInteractIcon();
            }
        }
    }
}