using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    #region Variables
    [Header("Enemys")]
    public Transform groundEnemy1;
    public Transform groundEnemy2;
    public Transform airEnemy;

    [Header("Spawns")]
    public Transform groundSpawnPoint;
    public Transform[] aerialSpawnPoints;
    private Transform selectedAerialSpawn;
    
    [Header("Times")]
    public float timeBetweenWaves = 5;
    public float timeForWaveSpawn = 0.5f;
    public float initialCountDown = 5f;
    public Text waveCounterText;

    private int waveIndex = 0;
    [Header("Max Wave")]
    public int waveThreshold = 10;

    public GameObject[] groundEnemiesOnScreen;
    public GameObject[] airEnemiesOnScreen;

    public int enemiesAlive = 0;

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
        if(commenceWaves)
        {
            initialCountDown -= Time.deltaTime;
            initialCountDown = Mathf.Clamp(initialCountDown, 0f, Mathf.Infinity);
            waveCounterText.text = string.Format("{0:00.00}", initialCountDown);
        }

        if (initialCountDown <= 0f && waveIndex < waveThreshold)
        {
            StartCoroutine(SpawnWave());
            initialCountDown = timeBetweenWaves;
        }

        groundEnemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy");
        airEnemiesOnScreen = GameObject.FindGameObjectsWithTag("AirEnemy");
        enemiesAlive = groundEnemiesOnScreen.Length + airEnemiesOnScreen.Length;

        if(waveIndex == waveThreshold && enemiesAlive < 1)
        {
            SceneManager.LoadScene(5);
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
