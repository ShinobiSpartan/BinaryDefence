using UnityEngine;

[System.Serializable]
public class Wave
{
    public float enemySpawnRate;

    [Header("Ground Enemy (Basic)")]
    public GameObject ground1;
    public int ground1Count;

    [Header("Ground Enemy (Tank)")]
    public GameObject ground2;
    public int ground2Count;

    [Header("Air Enemy")]
    public GameObject airEnemy;
    public int airEnemyCount;
}
