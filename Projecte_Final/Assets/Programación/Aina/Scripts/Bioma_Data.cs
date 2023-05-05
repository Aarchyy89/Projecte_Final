using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bioma_Data : MonoBehaviour
{
    [SerializeField, Tooltip("What the biome produces")]
    private string biomaType;
    
    [SerializeField, Tooltip("The line / circle that the biome is on")]
    private int biomaLine;

    [SerializeField, Tooltip("Starter and current biome")]
    private GameObject gameObjectBioma;
    
    [SerializeField, Tooltip("Game Object of the biome when it's unlocked")]
    private GameObject biomaUnlocked;
    
    [SerializeField, Tooltip("Game Object of the biome when it's built")]
    private GameObject biomaBuilt;
    
    [SerializeField, Tooltip("Highlighted hexagon prefab to show selected biomes")]
    private GameObject selectedPrefab;
    
    [SerializeField, Tooltip("Game Object of the tower when it's built")]
    private GameObject towerPrefab;
    
    [SerializeField, Tooltip("A list of the biomes around to check if you can unlock")]
    private List<Bioma_Data> nearBiomaList;
    
    [SerializeField, Tooltip("Bool to check if any of the biomes around are unlocked to be able to unlock this one")]
    private bool isAvailable;
    
    [SerializeField, Tooltip("Bool to check if the biome is unlocked to be able to build")]
    private bool isUnlocked;
    
    [SerializeField, Tooltip("Bool to check if the biome is built to start producing resources")]
    private bool isBuilt;

    [SerializeField, Tooltip("Maximum number of total resources the parcel can produce")]
    private int resourcesMax;
    
    [SerializeField, Tooltip("Number of resources the parcel produces after x time")]
    private int resourcesRound;

    [SerializeField, Tooltip("Number of time between extracting resources")]
    private float resourcesTime;
    
    [SerializeField, Tooltip("Number of wood you need to unlock the parcel")]
    private int costWoodUnlock;

    [SerializeField, Tooltip("Number of stones you need to unlock the parcel")]
    private int costStoneUnlock;
    
    [SerializeField, Tooltip("Number of wood you need to build on the parcel")]
    private int costWoodBuilt;
    
    [SerializeField, Tooltip("Number of stones you need to build on the parcel")]
    private int costStoneBuilt;

    // Getters
    
    public string BiomaType => biomaType;
    
    public int BiomaLine => biomaLine;

    public GameObject BiomaUnlocked => biomaUnlocked;
    
    public GameObject BiomaBuilt => biomaBuilt;
    
    public GameObject SelectedPrefab => selectedPrefab;
    
    public GameObject TowerPrefab => towerPrefab;
    
    public List<Bioma_Data> NearBiomaList => nearBiomaList;

    public bool IsUnlocked => isUnlocked;
    
    public bool IsAvailable => isAvailable;
    
    public bool IsBuilt => isBuilt;
    
    public int ResourcesMax => resourcesMax;
    
    public int ResourcesRound => resourcesRound;
    
    public float ResourcesTime => resourcesTime;
    
    public int CostWoodUnlock => costWoodUnlock;

    public int CostStoneUnlock => costStoneUnlock;
    
    public int CostWoodBuilt => costWoodBuilt;

    public int CostStoneBuilt => costStoneBuilt;

    private void Start()
    {
        GetNearBiomes();
    }

    private void GetNearBiomes()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 1);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != GetComponent<Collider>() && hitCollider != null)
            {
                nearBiomaList.Add(hitCollider.GetComponent<Bioma_Data>());
            }
        }
    }

    public void EnableUnlockNearBiomes()
    {
        foreach (var bioma in nearBiomaList)
        {
            if (!bioma.isUnlocked)
            {
                bioma.isUnlocked = true;
            }
        }
    }
}

