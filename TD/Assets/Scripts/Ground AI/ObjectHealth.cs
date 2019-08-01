using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public float health;

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
