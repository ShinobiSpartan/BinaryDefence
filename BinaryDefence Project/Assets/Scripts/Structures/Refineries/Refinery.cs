using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Refinery : MonoBehaviour
{
    public int resourcesGainedPerPull = 10;
    public float timeBetweenResourceCollection = 5f;
    private float timer;

    public Image currencyIndicator;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerStats>();
        timer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (currencyIndicator.color.a > 0)
        {
            var tempColor = currencyIndicator.color;
            tempColor.a -= 0.025f;
            currencyIndicator.color = tempColor;

        }

        if (timer >= timeBetweenResourceCollection)
        {
            playerStats.AddMoney(resourcesGainedPerPull);

            var tempColor = currencyIndicator.color;
            tempColor.a = 1;
            currencyIndicator.color = tempColor;

            timer -= timeBetweenResourceCollection;
        }

    }
}
