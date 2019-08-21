using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public enum SpawnState { Spawn, Wait, Count}


   [System.Serializable]
   public class Wave
   {
        public string name;
        public Transform enemy;
        public Transform enemy2;
        public Transform enemy3;
        public int count;
        public float rate;
   }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoint;

    public float wavePauseTime = 5.0f;
    private float nextWaveCountDown;

    private float searchCounter = 1.0f;

    private SpawnState state = SpawnState.Count;

    void Start()
    {
        if (spawnPoint.Length == 0)
        {
            Debug.LogError("NO SPAWN POINTS");
        }

        //time between waves which is defaulted to 5 seconds
        nextWaveCountDown = wavePauseTime;
    }

    void Update()
    {
        if(state == SpawnState.Wait)
        {
            //checking if there is anything alive
            if(AliveEnemys() == false)
            {
                //start a new round
                WaveDone();
                return;
            }
            else
            {
                return;
            }

        }



        //if we hit the timer at 0, check to see if anything is spawning
        if(wavePauseTime <=0)
        {
            if(state != SpawnState.Spawn)
            {
                //lets start spawning wave(s)!
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            //count down from 5
            nextWaveCountDown -= Time.deltaTime;
        }
    }

    /// <summary>
    /// checking if the enemy(s) is alive 
    /// if we have any spawned enemys on the map/playfeild
    /// </summary>
    /// <returns></returns>
    bool AliveEnemys()
    {
        searchCounter -= Time.deltaTime;

        if(searchCounter <= 0)
        {
            searchCounter = 1f;
            if(GameObject.FindGameObjectsWithTag("Enemey") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Wave: " + _wave.name);
        //actually spawning stuff
        state = SpawnState.Spawn;

        //spawn how many mobs/enemys 
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnMob(_wave.enemy);
            //need to wait X amount of seconds
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.Wait;

        yield break;
    }

    void WaveDone()
    {
        Debug.Log("Wave done");

        state = SpawnState.Count;
        nextWaveCountDown = wavePauseTime;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            //might have to add some sort of end screen here
            Debug.Log("All waves complete!");
            ///SceneManager.LoadScene("GameOver");
        }
        else
        {
            nextWave++;
        }

    }

    void SpawnMob(Transform enemy)
    {
        Debug.Log("Spawned: " +enemy.name);

        //Spawn the enemys here!
        Transform spawn = spawnPoint[Random.Range (0,spawnPoint.Length)];
        Instantiate(enemy, transform.position, transform.rotation);
    }

}
