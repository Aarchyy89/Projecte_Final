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

    private GameObject uiBackup;

    [SerializeField, Tooltip("A list of the biomes around to check if you can unlock")]
    private List<Bioma_Data> nearBiomaList;
    
    [SerializeField, Tooltip("Bool to check if any of the biomes around are unlocked to be able to unlock this one")]
    private bool isAvailable;
    
    [SerializeField, Tooltip("Bool to check if the biome is unlocked to be able to build")]
    private bool isUnlocked;
    
    [SerializeField, Tooltip("Bool to check if the biome is built to start producing resources")]
    private bool isBuilt;
    
    [SerializeField, Tooltip("Bool to check if the biome can produce resources")]
    private bool canProduce;
    
    [SerializeField, Tooltip("Bool to check if the biome can produce resources")]
    private bool isProducing;

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
    
    [SerializeField, Tooltip("Number of wood you need to build a tower")]
    private int costWoodTower;
    
    [SerializeField, Tooltip("Number of stones you need to build a tower")]
    private int costStoneTower;

    // Getters
    
    public string BiomaType => biomaType;
    
    public List<Bioma_Data> NearBiomaList => nearBiomaList;

    public bool IsUnlocked => isUnlocked;
    
    public bool IsAvailable => isAvailable;
    
    public bool IsBuilt => isBuilt;
    
    public bool CanProduce => canProduce;

    public bool IsProducing
    {
        get {return isProducing; }
        set {isProducing = value; }
    }
    
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
        if (!isAvailable)
        {
            GameObject clouds = PoolingManager.Instance.GetPooledObject((int)biomaClouds);
            clouds.transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
            clouds.transform.parent = transform;
            clouds.gameObject.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        if (isAvailable)
        {
            GameObject ui = PoolingManager.Instance.GetPooledObject((int)UI);
            ui.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            uiBackup = ui;

            if (!isUnlocked)
            {
                ui.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{costWoodUnlock}";
                ui.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{costStoneUnlock}";
                ui.gameObject.SetActive(true);
                
                if (GameManager.instance.WoodPlayer >= costWoodUnlock && GameManager.instance.StonePlayer >= costStoneUnlock)
                {
                    ui.gameObject.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(UnlockHexagon);
                   
                    GameObject able = PoolingManager.Instance.GetPooledObject((int)selectedAble);
                    able.transform.position = transform.position;
                    able.SetActive(true);
                }
                else
                {
                    GameObject unable = PoolingManager.Instance.GetPooledObject((int)selectedUnable);
                    unable.transform.position = transform.position;
                    unable.SetActive(true);
                }
            }
            else if(isUnlocked && canProduce && !isBuilt)
            {
                ui.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{costWoodBuilt}";
                ui.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{costStoneBuilt}";
                ui.gameObject.SetActive(true);

                if (GameManager.instance.WoodPlayer >= costWoodBuilt && GameManager.instance.StonePlayer >= costStoneBuilt)
                {
                    ui.gameObject.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(BuildHexagon);
                   
                    GameObject able = PoolingManager.Instance.GetPooledObject((int)selectedAble);
                    able.transform.position = transform.position;
                    able.SetActive(true);
                }
                else
                {
                    GameObject unable = PoolingManager.Instance.GetPooledObject((int)selectedUnable);
                    unable.transform.position = transform.position;
                    unable.SetActive(true);
                }
            }
            else if (isUnlocked && !canProduce && !isBuilt)
            {
                    
            }
            else if (!isProducing && isBuilt && resourcesMax <= 0)
            {
                ui.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{costWoodBuilt}";
                ui.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{costStoneBuilt}";
                ui.gameObject.SetActive(true);

                if (GameManager.instance.WoodPlayer >= costWoodBuilt && GameManager.instance.StonePlayer >= costStoneBuilt)
                {
                    ui.gameObject.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(BuildHexagon);
                   
                    GameObject able = PoolingManager.Instance.GetPooledObject((int)selectedAble);
                    able.transform.position = transform.position;
                    able.SetActive(true);
                }
                else
                {
                    GameObject unable = PoolingManager.Instance.GetPooledObject((int)selectedUnable);
                    unable.transform.position = transform.position;
                    unable.SetActive(true);
                }
            }
        }
    }

    private void UnlockHexagon()
    {
        GameManager.instance.WoodPlayer -= costWoodUnlock;
        GameManager.instance.StonePlayer -= costStoneUnlock;
        uiBackup.SetActive(false);
        MakeAvailableNearBiomes();
    }
    
    private void BuildHexagon()
    {
        GameManager.instance.WoodPlayer -= costWoodBuilt;
        GameManager.instance.StonePlayer -= costStoneBuilt; 
        
        GameObject build = PoolingManager.Instance.GetPooledObject((int)biomaBuilt);
        build.transform.position = transform.position;
        build.SetActive(true);
        
        // Call resources Coroutine
    }
}

