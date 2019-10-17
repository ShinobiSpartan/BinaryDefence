using UnityEngine;

[System.Serializable]
public class Wave
{
    [Header("Ground Enemy (Basic)")]
    public GameObject ground1;
    public int ground1Count;
    [Tooltip("Lowering the Rate will increase time between unit spawns")]
    public float ground1Rate;

    [Header("Air Enemy (Basic)")]
    public GameObject airEnemy;
    public int airEnemyCount;
    [Tooltip("Lowering the Rate will increase time between unit spawns")]
    public float airEnemyRate;

    [Header("Ground Enemy (Tank)")]
    public GameObject ground2;
    public int ground2Count;
    [Tooltip("Lowering the Rate will increase time between unit spawns")]
    public float ground2Rate;

}
