using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Money")]
    //static money
    public static int money;
    //starting money for the player
    public int startingMoney = 500;

    void Start()
    {
        //Giving the player money to start with
        money = startingMoney;
    }
    /// <summary>
    /// Giving money to the player
    /// </summary>
    /// <param name="valueToAdd"></param>
    public void AddMoney(int valueToAdd)
    {
        //adding money
        money += valueToAdd;
        Debug.Log("Money added. New total is: " + money);
    }

}
