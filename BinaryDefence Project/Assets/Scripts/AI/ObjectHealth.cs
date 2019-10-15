using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectHealth : MonoBehaviour
{
    private float currentHealth;
    public float fullHealth;

    public Image healthBar;

    private void Awake()
    {
        currentHealth = fullHealth;
    }


    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;
        healthBar.fillAmount = currentHealth / fullHealth;

        if (currentHealth <= 0)
        {
            if(this.gameObject.tag == "BaseStruct")
            {
                SceneManager.LoadScene(4);
                return;
            }
            else
            {
                WaveSpawner.EnemiesAlive--;

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
