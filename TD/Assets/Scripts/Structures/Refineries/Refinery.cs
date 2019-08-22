using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refinery : MonoBehaviour
{
    public int resourcesGainedPerPull = 10;
    public float timeBetweenResourceCollection = 5f;
    private float timer;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerStats>();
        timer = timeBetweenResourceCollection;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenResourceCollection)
        {
            timer -= timeBetweenResourceCollection;
            playerStats.AddMoney(resourcesGainedPerPull);
        }

    }
}
