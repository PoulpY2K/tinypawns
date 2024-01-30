using System.Collections;
using System.Collections.Generic;
using Entity.Interfaces;
using UnityEngine;

public class Lootable : MonoBehaviour, ILootable
{
    public float quantity = 1;
    
    private Animator _animator;
    private Rigidbody2D _rb;
    
    public float Quantity
    {
        get => quantity;
        set
        {
            quantity = value;
        }
    }
}
