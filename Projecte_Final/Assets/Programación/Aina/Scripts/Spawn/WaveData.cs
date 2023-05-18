using UnityEngine;

[CreateAssetMenu(fileName = "new Wave Data", menuName = "Wave Data")]
public class WaveData : ScriptableObject
{
    [SerializeField, Tooltip("Float variable of minimum waiting time between instances")]
    private float spawnTimer_min;

    [SerializeField, Tooltip("Variable float of maximum waiting time between instances")]
    private float spawnTimer_max;
    
    [SerializeField, Tooltip("Indicator of total number of boats that will be instantiated in that wave, each boat carries x enemies")]
    private int totalEnemyBoats;
    
    [SerializeField, Tooltip("Indicator of total number of enemies inside the boat")]
    private int totalEnemies;

    [SerializeField, Tooltip("Float variable of minimum waiting time between instances")]
    private int enemyAttackStats;

    [SerializeField, Tooltip("Variable float of maximum waiting time between instances")]
    private int enemySpeedStats;

    // Getters
    
    public float SpawnTimer_min => spawnTimer_min;
    
    public float SpawnTimer_max => spawnTimer_max;
    
    public int TotalEnemyBoats => totalEnemyBoats;
    
    public int TotalEnemies => totalEnemies;
    
    public int EnemyAttackStats => enemyAttackStats;
    
    public int EnemySpeedStats => enemySpeedStats;
}
