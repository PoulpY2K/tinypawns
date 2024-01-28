using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Health
    {
        set
        {
            health = value;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        get { return health; }
    }

    public float health = 3f;

    private void OnHit(float damage)
    {
        Health -= damage;
    }
}