using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectHealth : MonoBehaviour
{
    public float currentHealth;
    public float fullHealth;

    private void Awake()
    {
        currentHealth = fullHealth;
    }


    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            if(this.gameObject.tag == "BaseStruct")
            {
                SceneManager.LoadScene(SceneManager.GetSceneByName("Lobby").buildIndex);
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
