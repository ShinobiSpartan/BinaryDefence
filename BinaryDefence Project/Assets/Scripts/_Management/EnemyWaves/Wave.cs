using UnityEngine;

[System.Serializable]
public class Wave
{
    [Header("Ground Enemy (Basic)")]
    public GameObject ground1;
    public int ground1Count;
    public float ground1Rate;

    [Header("Air Enemy")]
    public GameObject airEnemy;
    public int airEnemyCount;
    public float airEnemyRate;

    [Header("Ground Enemy (Tank)")]
    public GameObject ground2;
    public int ground2Count;
    public float ground2Rate;

}
