using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    #region Variables

    public Wave[] waves;

    [Header("Spawns")]
    public Transform spawnPoint;
   // public Transform[] aerialSpawnPoints;
   // private Transform selectedAerialSpawn;
    
    [Header("Times")]
    public float timeBetweenWaves = 5;
    public float countdown = 5f;
    public Text waveCounterText;

    private int waveIndex = 0;
    [Header("Max Wave")]
    public int waveThreshold = 10;

    public Button startWavesButton;
    bool commenceWaves = false;
    #endregion

    private void OnEnable()
    {
        Time.timeScale = 0;
        startWavesButton.onClick.AddListener(delegate { StartWaves(); });
    }

    private void Update()
    {
        if(EnemiesAlive > 0)
        {
            return;
        }

        if(commenceWaves)
        {
            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            waveCounterText.text = string.Format("{0:00.00}", countdown);
        }


        if (countdown <= 0f && waveIndex < waveThreshold)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

       // if(waveIndex == waveThreshold && EnemiesAlive < 1)
       // {
       //     SceneManager.LoadScene(5);
       //     return;
       // }
    }

    private void StartWaves()
    {
        commenceWaves = true;
        Time.timeScale = 1;
        startWavesButton.gameObject.SetActive(false);
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.enemyCount; i++)
        {
            //SelectNextAerialSpawn();
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        waveIndex++;

        if(waveIndex == waves.Length)
        {
            Debug.Log("Level Won!");
            SceneManager.LoadScene(5);
        }
    }
   
   // void SelectNextAerialSpawn()
   // {
   //     int spawnPointIndex = Random.Range(0, aerialSpawnPoints.Length);
   //     selectedAerialSpawn = aerialSpawnPoints[spawnPointIndex];
   // }

    /// <summary>
    /// Spawn the enemy at the starting position
    /// </summary>
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;

    }



}
