using UnityEngine;

[CreateAssetMenu(fileName = "new Wave Data", menuName = "Wave Data")]
public class WaveData : ScriptableObject
{
    [SerializeField, Tooltip("Indicator of total number of enemies that will be instantiated in that wave")]
    private int totalEnemies;

    [SerializeField, Tooltip("Float variable of minimum waiting time between instances")]
    private float spawnTimer_min;

    [SerializeField, Tooltip("Variable float of maximum waiting time between instances")]
    private float spawnTimer_max;

    // Getters
    
    public int TotalEnemies => totalEnemies;
    
    public float SpawnTimer_min => spawnTimer_min;
    
    public float SpawnTimer_max => spawnTimer_max;
}
