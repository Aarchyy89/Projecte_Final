using UnityEngine;

[CreateAssetMenu(fileName = "new Trigger Data", menuName = "Trigger Data")]
public class Trigger_Data : ScriptableObject
{
    [SerializeField, Tooltip("Total hexagons build")]
    private int totalConstructions;
    
    [SerializeField, Tooltip("Total towers build")]
    private int totalTowers;    
    
    // Getters

    public int TotalConstructions => totalConstructions;
    
    public int TotalTowers => totalTowers;
}
