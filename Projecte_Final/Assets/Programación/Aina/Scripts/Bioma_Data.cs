using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Bioma Data", menuName = "Bioma Data")]
public class Bioma_Data : ScriptableObject
{
    [SerializeField, Tooltip("")]
    private string biomaType;    
    
    [SerializeField, Tooltip("")]
    private GameObject biomaPrefab;    
    
    [SerializeField, Tooltip("")]
    private bool isUnlocked;    
    
    [SerializeField, Tooltip("")]
    private bool isBuilt;

    [SerializeField, Tooltip("")]
    private int resourcesMax;

    [SerializeField, Tooltip("")]
    private float resourcesTime;
    
    [SerializeField, Tooltip("")]
    private int costWood;    
    
    [SerializeField, Tooltip("")]
    private int costStone;

    // Getters
    
    public string BiomaType => biomaType;
    
    public GameObject BiomaPrefab => biomaPrefab;
    
    public bool IsUnlocked => isUnlocked;
    
    public bool IsBuilt => isBuilt;
    
    public int ResourcesMax => resourcesMax;
    
    public float ResourcesTime => resourcesTime;
    
    public int CostWood => costWood;

    public int CostStone => costStone;
}

