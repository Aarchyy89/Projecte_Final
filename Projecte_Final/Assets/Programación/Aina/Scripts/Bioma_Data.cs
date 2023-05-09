using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bioma_Data : MonoBehaviour
{
    [SerializeField, Tooltip("What the biome produces")]
    private string biomaType;

    [SerializeField, Tooltip("Starter and current biome")]
    private PoolingItemsEnum biomaClouds;

    [SerializeField, Tooltip("Game Object of the biome when it's built")]
    public PoolingItemsEnum biomaBuilt;

    [SerializeField, Tooltip("Game Object of the tower when it's built")]
    public PoolingItemsEnum towerPrefab;
    
    [SerializeField, Tooltip("Game Object of the tower when it's built")]
    public PoolingItemsEnum selectedAble;
    
    [SerializeField, Tooltip("Game Object of the tower when it's built")]
    public PoolingItemsEnum selectedUnable;
    
    [SerializeField, Tooltip("Game Object of the tower when it's built")]
    public PoolingItemsEnum UI;

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
        PoolItems();
    }

    private void GetNearBiomes()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 1);
        
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != GetComponent<Collider>() && hitCollider.GetComponent<Bioma_Data>() != null)
            {
                nearBiomaList.Add(hitCollider.GetComponent<Bioma_Data>());
            }
        }
    }

    private void MakeAvailableNearBiomes()
    {
        foreach (var bioma in nearBiomaList)
        {
            if (!bioma.isAvailable)
            {
                bioma.isAvailable = true;
                bioma.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    
    void PoolItems()
    {
        GameObject clouds = PoolingManager.Instance.GetPooledObject((int)biomaClouds);
        GameObject resources = PoolingManager.Instance.GetPooledObject((int)biomaBuilt);
        GameObject tower = PoolingManager.Instance.GetPooledObject((int)towerPrefab);
        GameObject ui = PoolingManager.Instance.GetPooledObject((int)UI);


        clouds.transform.position = gameObject.transform.position;
        clouds.transform.parent = gameObject.transform;
        resources.transform.position = gameObject.transform.position;
        resources.transform.parent = gameObject.transform;
        tower.transform.position = gameObject.transform.position;
        tower.transform.parent = gameObject.transform;
        ui.transform.position = gameObject.transform.position;
        ui.transform.parent = gameObject.transform;
        clouds.gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        Debug.Log("here");
        if (isAvailable)
        {
            Canvas ui = gameObject.transform.GetChild(3).GetComponent<Canvas>();
            ui.transform.position = new Vector3(0, 1, 0);
            
            if (!isUnlocked)
            {
                ui.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{costWoodUnlock}";
                ui.gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{costStoneUnlock}";
                ui.gameObject.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(UnlockHexagon);
                ui.gameObject.SetActive(true);
            }
            else if(isUnlocked && !isBuilt)
            {
                ui.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{costWoodBuilt}";
                ui.gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{costStoneBuilt}";
                ui.gameObject.SetActive(true);
            }
            else if (isBuilt && resourcesMax <= 0)
            {
                
            }
        }
    }

    private void UnlockHexagon()
    {
        // check if current wood is same or more cost wood
        // if true take price and unlock the hexagon
        MakeAvailableNearBiomes();
    }
}

