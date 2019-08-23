using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public int startingMoney = 2000;

    void Start()
    {
        //Giving the player money to start with
        money = startingMoney;
    }

    public void AddMoney(int valueToAdd)

    {
        money += valueToAdd;
        Debug.Log("Money added. New total is: " + money);
    }

}
