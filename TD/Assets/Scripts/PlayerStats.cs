using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int addMoney = 50;
    public float incomeDelay = 10.0f;

    public static int money;
    public int startingMoney = 500;

    public GameObject Ref;

    void Start()
    {
        //giving the player money to start with
        money = startingMoney;
    }


    public void AddingMoney()
    {
        incomeDelay = Time.deltaTime;

        if(incomeDelay == 0)
        {
            money += addMoney;
        }

        Debug.Log("Money added. New total is: " + money);

    }

}
