using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    #region Variables
    public Transform enemy;
    public Transform spawnPoint;
    
    public float timeBetweenWaves = 5;
    public float timeForWaveSpawn = 0.5f;
    private float countDown = 2f;
    public Text waveCounterText;

    private int waveIndex = 0;
    #endregion

    private void Update()
    {
        if(countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }
        countDown -= Time.deltaTime;
        waveCounterText.text = Mathf.Round(countDown).ToString();
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
    }



}
