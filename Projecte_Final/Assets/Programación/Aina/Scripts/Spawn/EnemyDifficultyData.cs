using UnityEngine;

[CreateAssetMenu(fileName = "new Enemy Difficulty Data", menuName = "Enemy Difficulty Data")]
public class EnemyDifficultyData : ScriptableObject
{
    [SerializeField, Tooltip("Total hexagons build")]
    private int totalConstructions;
    
    [SerializeField, Tooltip("Total towers build")]
    private int totalTowers;    
    
    [SerializeField, Tooltip("Actual town hall level")]
    private int lvlTownHall;

    // Getters

    public int TotalConstructions => totalConstructions;
    
    public int TotalTowers => totalTowers;
    
    public int LvlTownHall => lvlTownHall;
}
