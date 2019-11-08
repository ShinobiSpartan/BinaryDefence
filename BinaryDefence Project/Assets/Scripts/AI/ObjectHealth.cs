using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectHealth : MonoBehaviour
{
    private float currentHealth;
    public float fullHealth;
    
    // Referencing WaveSpawner
    public WaveSpawner ws;

    bool fullyDead = false;
    public Image healthBar;

    private void Awake()
    {
        currentHealth = fullHealth;
    }

    private void Start()
    {
        // Finding WaveSpawner script
        ws = FindObjectOfType<WaveSpawner>();
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;
        healthBar.fillAmount = currentHealth / fullHealth;

        if (currentHealth <= 0)
        {
            // Checking to see if NPC(s) is fully dead once(not twice)
            if(fullyDead == false)
            {
                HasDied(); 
                fullyDead = true;
            }
            // If base is dead
            if (this.gameObject.tag == "BaseStruct")
            {
                SceneManager.LoadScene("Lose");
                return;
            }
        }
    }
    /// <summary>
    /// target has died + destroy gameobject
    /// </summary>
    public void HasDied()
    {
        ws.EnemiesAlive--;
        Destroy(this.gameObject);
    }
}
