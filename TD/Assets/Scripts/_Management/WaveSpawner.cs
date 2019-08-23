using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    #region Variables
    public Transform enemy;
    public Transform enemy2;
    public Transform enemy3;
    public Transform spawnPoint;
    
    public float timeBetweenWaves = 5;
    public float timeForWaveSpawn = 0.5f;
    public float initialCountDown = 5f;
    public Text waveCounterText;

    private int waveIndex = 0;
    public int waveThreshold = 10;

    public GameObject[] groundEnemiesOnScreen;
    public GameObject[] airEnemiesOnScreen;

    public int enemiesAlive = 0;
    #endregion

    private void Update()
    {
        if(initialCountDown <= 0f && waveIndex < waveThreshold)
        {
            StartCoroutine(SpawnWave());
            initialCountDown = timeBetweenWaves;
        }
        initialCountDown -= Time.deltaTime;

        initialCountDown = Mathf.Clamp(initialCountDown, 0f, Mathf.Infinity);

        waveCounterText.text = string.Format("{0:00.00}", initialCountDown);

        groundEnemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy");
        airEnemiesOnScreen = GameObject.FindGameObjectsWithTag("AirEnemy");
        enemiesAlive = groundEnemiesOnScreen.Length + airEnemiesOnScreen.Length;
    }


    IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeForWaveSpawn);
        }

        Debug.Log("Spawning!");
    }
    
    /// <summary>
    /// Spawn the enemy at the starting position
    /// </summary>
    void SpawnEnemy()
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        Instantiate(enemy2, spawnPoint.position, spawnPoint.rotation);
        Instantiate(enemy3, spawnPoint.position, spawnPoint.rotation);

    }



}
