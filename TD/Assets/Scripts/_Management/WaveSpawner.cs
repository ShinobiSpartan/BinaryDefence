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
    private float countDown = 2f;
    public Text waveCounterText;

    private int waveIndex = 0;

    public GameObject[] groundEnemiesOnScreen;
    public GameObject[] airEnemiesOnScreen;

    public int enemiesAlive = 0;
    #endregion

    private void Update()
    {
        if(countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }
        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        waveCounterText.text = string.Format("{0:00.00}", countDown);

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
