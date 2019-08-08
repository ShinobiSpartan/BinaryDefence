using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public float currentHealth;
    public float fullHealth;

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            if(this.gameObject.tag == "BaseStruct")
            {
                // Insert code for lose state here

            }
            else
            {
                Destroy(this.gameObject);
            }

        }
    }

    public float DisplayHealth()
    {
        float healthAsPercent;
        healthAsPercent = (currentHealth / fullHealth) * 100;

        return healthAsPercent;
    }
}
