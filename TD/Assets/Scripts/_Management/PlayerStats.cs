using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static float money;
    public int startingMoney = 2000;
    
    void Start()
    {
        //giving the player money to start with
        money = startingMoney;
    }

    public void AddIncome(float moneyIncome)
    {
        moneyIncome += money;
    }

}
