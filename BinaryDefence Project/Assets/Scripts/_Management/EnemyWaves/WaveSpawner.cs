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
    public Transform[] aerialSpawnPoints;
    private Transform selectedAerialSpawn;
    
    [Header("Times")]
    public float timeBetweenWaves = 5;
    public float countdown = 5f;
    public Text waveCounterText;

    private int waveIndex = 0;

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

        if (waveIndex == waves.Length)
        {
            Debug.Log("Level Won!");
            SceneManager.LoadScene("Win");
        }

        if (commenceWaves)
        {
            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            waveCounterText.text = string.Format("{0:00.00}", countdown);
        }


        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
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

        for (int i = 0; i < wave.ground1Count; i++)
        {
            SpawnEnemy(wave.ground1);
            yield return new WaitForSeconds(1f / wave.enemySpawnRate);
        }
        
        for (int i = 0; i < wave.airEnemyCount; i++)
        {
            SelectNextAerialSpawn();
            SpawnAirEnemy(wave.airEnemy);
            yield return new WaitForSeconds(1f / wave.enemySpawnRate);
        }
        
        for (int i = 0; i < wave.ground2Count; i++)
        {
            SpawnEnemy(wave.ground2);
            yield return new WaitForSeconds(1f / wave.enemySpawnRate);
        }

        waveIndex++;
    }
   
    void SelectNextAerialSpawn()
    {
        int spawnPointIndex = Random.Range(0, aerialSpawnPoints.Length);
        selectedAerialSpawn = aerialSpawnPoints[spawnPointIndex];
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }

    void SpawnAirEnemy(GameObject enemy)
    {
        Instantiate(enemy, selectedAerialSpawn.position, selectedAerialSpawn.rotation);
        EnemiesAlive++;
    }
}
