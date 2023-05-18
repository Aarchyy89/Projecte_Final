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
    public PoolingItemsEnum biomaUnlocked;
        
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
    
    [SerializeField, Tooltip("Bool to check if the biome is built to start producing resources")]
    private bool canBuild;
    
    [SerializeField, Tooltip("Bool to check if the biome has a tower")]
    private bool isTower;

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

    private GameObject ui_local;
    private GameObject able_local;
    private GameObject unable_local;
    private GameObject unlocked_local;
    private GameObject build_local;
    private GameObject tower_local;

    // Getters
    
    public string BiomaType => biomaType;
    
    public List<Bioma_Data> NearBiomaList => nearBiomaList;

    public bool IsUnlocked => isUnlocked;
    
    public bool IsAvailable => isAvailable;
    
    public bool IsBuilt => isBuilt;
    
    public bool CanBuild => canBuild;
    
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
            clouds.transform.position = transform.position;
            clouds.transform.parent = transform;
            clouds.gameObject.SetActive(true);
        }
    }
    
    private void ActivateUI(int costWood, int costStone)
    {
        ui_local = PoolingManager.Instance.GetPooledObject((int)UI);
        ui_local.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        
        ui_local.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{costWood}";
        ui_local.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{costStone}";
        ui_local.gameObject.SetActive(true);
    }
    
    private void DeactivateUI()
    {
        PoolingManager.Instance.DesactivatePooledObject((int)UI);
        PoolingManager.Instance.DesactivatePooledObject((int)selectedAble);
        PoolingManager.Instance.DesactivatePooledObject((int)selectedUnable);
        PoolingManager.Instance.RemoveListener((int)UI);
    }

    private void Instantiate_Able()
    {
        able_local = PoolingManager.Instance.GetPooledObject((int)selectedAble);
        able_local.transform.position = transform.position;
        able_local.SetActive(true);
    }
    
    private void Instantiate_Unable()
    {
        unable_local = PoolingManager.Instance.GetPooledObject((int)selectedUnable);
        unable_local.transform.position = transform.position;
        unable_local.SetActive(true);
    }
    
    private void Instantiate_Build(GameObject parent, PoolingItemsEnum itemsEnum)
    {
        parent = PoolingManager.Instance.GetPooledObject((int)itemsEnum);
        parent.transform.position = transform.position;
        parent.SetActive(true);
    }
    

    private void OnMouseDown()
    {
        DeactivateUI();

        if (isAvailable)
        {
            if (!isUnlocked)
            {
                ActivateUI(costWoodUnlock,costStoneUnlock);
                
                if (GameManager.instance.WoodPlayer >= costWoodUnlock && GameManager.instance.StonePlayer >= costStoneUnlock)
                {
                    ui_local.gameObject.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(UnlockHexagon);
                   
                    Instantiate_Able();
                }
                else
                {
                    Instantiate_Unable();
                }

            }
            else if(isUnlocked && !isBuilt && canBuild)
            {
                ActivateUI(costWoodBuilt,costStoneBuilt);

                if (GameManager.instance.WoodPlayer >= costWoodBuilt && GameManager.instance.StonePlayer >= costStoneBuilt)
                {
                    ui_local.gameObject.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(BuildHexagon);
                   
                    Instantiate_Able();
                }
                else
                {
                    Instantiate_Unable();
                }
            }
            else if(isUnlocked && !isBuilt && !canBuild && !isTower || isBuilt && resourcesMax <= 0 && !isTower)
            {
                ActivateUI(costWoodTower,costStoneTower);

                if (GameManager.instance.WoodPlayer >= costWoodTower && GameManager.instance.StonePlayer >= costStoneTower)
                {
                    ui_local.gameObject.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(BuildTower);
                   
                    Instantiate_Able();
                }
                else
                {
                    Instantiate_Unable();
                }
            }
        }
    }

    private void UnlockHexagon()
    {
        GameManager.instance.WoodPlayer -= costWoodUnlock;
        GameManager.instance.StonePlayer -= costStoneUnlock;

        isUnlocked = true;
        
        ui_local.SetActive(false);
        able_local.SetActive(false);

        Instantiate_Build(unlocked_local, biomaUnlocked);
        MakeAvailableNearBiomes();
    }
    
    private void BuildHexagon()
    {
        GameManager.instance.WoodPlayer -= costWoodBuilt;
        GameManager.instance.StonePlayer -= costStoneBuilt;
        GameManager.instance.TotalConstructions += 1;

        isBuilt = true;
        
        ui_local.SetActive(false);
        able_local.SetActive(false);

        Instantiate_Build(build_local, biomaBuilt);
        //Resources_Controller.instance.
    }
    
    private void BuildTower()
    {
        GameManager.instance.WoodPlayer -= costWoodTower;
        GameManager.instance.StonePlayer -= costStoneTower;
        GameManager.instance.TotalTowers += 1;
        
        isTower = true;

        ui_local.SetActive(false);
        able_local.SetActive(false);

        Instantiate_Build(tower_local, towerPrefab);
    }
}

