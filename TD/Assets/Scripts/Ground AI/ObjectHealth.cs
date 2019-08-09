using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;

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
