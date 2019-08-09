using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public int startingMoney = 500;

    void Start()
    {
        //giving the player money to start with
        money = startingMoney;
    }


}
