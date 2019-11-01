using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectHealth : MonoBehaviour
{
    private float currentHealth;
    public float fullHealth;

    bool fullyDead = false;
    //referencing WaveSpawner
    public WaveSpawner ws;

    public Image healthBar;

    private void Awake()
    {
        currentHealth = fullHealth;
    }

    private void Start()
    {
        //finding WaveSpawner script
        ws = FindObjectOfType<WaveSpawner>();
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;
        healthBar.fillAmount = currentHealth / fullHealth;

        if (currentHealth <= 0)
        {
            //checking to see if NPC(s) is fully dead once(not twice)
            if(fullyDead == false)
            {
                HasDied();
                fullyDead = true;
            }
            //if base is dead
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
