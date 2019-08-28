using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    #region Variables

    public Transform groundEnemy1;
    public Transform groundEnemy2;
    public Transform groundSpawnPoint;

    public Transform airEnemy;
    public Transform[] aerialSpawnPoints;
    private Transform selectedAerialSpawn;
    
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
            SelectNextAerialSpawn();
            SpawnEnemy();
            yield return new WaitForSeconds(timeForWaveSpawn);
        }

        Debug.Log("Spawning!");
    }
   
    void SelectNextAerialSpawn()
    {
        int spawnPointIndex = Random.Range(0, aerialSpawnPoints.Length);
        selectedAerialSpawn = aerialSpawnPoints[spawnPointIndex];
    }

    /// <summary>
    /// Spawn the enemy at the starting position
    /// </summary>
    void SpawnEnemy()
    {
        Instantiate(groundEnemy1, groundSpawnPoint.position, groundSpawnPoint.rotation);
        Instantiate(groundEnemy2, groundSpawnPoint.position, groundSpawnPoint.rotation);

        Instantiate(airEnemy, selectedAerialSpawn.position, selectedAerialSpawn.rotation);

    }



}
