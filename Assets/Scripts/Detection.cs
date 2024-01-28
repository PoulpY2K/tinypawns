using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public string targetTag = "Player";
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    public Collider2D col;

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