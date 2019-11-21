using UnityEngine;

[System.Serializable]
public class Wave
{
    [Header("Ground Enemy (Basic)")]
    //game object for first ground unit
    public GameObject ground1;
    //how many ground units
    public int ground1Count;
    [Tooltip("Lowering the Rate will increase time between unit spawns")]
    //spawn rate
    public float ground1Rate;

    [Header("Air Enemy (Basic)")]
    //game object for air enemy
    public GameObject airEnemy;
    //how many air enemies
    public int airEnemyCount;
    [Tooltip("Lowering the Rate will increase time between unit spawns")]
    //spawn rate
    public float airEnemyRate;

    [Header("Ground Enemy (Tank)")]
    //game object for second ground unit
    public GameObject ground2;
    //how many ground units
    public int ground2Count;
    [Tooltip("Lowering the Rate will increase time between unit spawns")]
    //spawn rate
    public float ground2Rate;

}
