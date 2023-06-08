using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bioma_Data : Resources_Controller
{
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
    
    [SerializeField, Tooltip("VFX of building process")]
    public PoolingItemsEnum VFX_Build;
    
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
    private GameObject tower_local;
    private GameObject VFXbuild_local;


    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioClip BUYsound;


    private void Start()
    {
        GetNearBiomes();
        PoolItems();
    }

    private void GetNearBiomes()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.75f);
        
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
            clouds.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            clouds.transform.parent = transform;
            clouds.gameObject.SetActive(true);
        }
    }
    
    private void ActivateUI(int costWood, int costStone)
    {
        ui_local = PoolingManager.Instance.GetPooledObject((int)UI);
        ui_local.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        
        ui_local.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{costWood}";
        ui_local.transform.GetChild(2).GetComponent<TMP_Text>().text = $"{costStone}";
        ui_local.gameObject.SetActive(true);
    }
    
    private void DeactivateUI()
    {
        PoolingManager.Instance.DesactivatePooledObject((int)UI);
        PoolingManager.Instance.DesactivatePooledObject((int)selectedAble);
        PoolingManager.Instance.DesactivatePooledObject((int)selectedUnable);
        PoolingManager.Instance.RemoveListener((int)UI, 3);
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
    
    private void Instantiate_Unlock()
    {
        unlocked_local = PoolingManager.Instance.GetPooledObject((int)biomaUnlocked);
        unlocked_local.transform.position = transform.position;
        unlocked_local.SetActive(true);
    }
    private void Instantiate_Build()
    {
        unlocked_local.SetActive(false);
        
        build_local = PoolingManager.Instance.GetPooledObject((int)biomaBuilt);
        build_local.transform.position = transform.position;
        build_local.SetActive(true);
    }
    private void Instantiate_Tower()
    {
        tower_local = PoolingManager.Instance.GetPooledObject((int)towerPrefab);
        tower_local.transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
        tower_local.SetActive(true);
    }
    
    private void Instantiate_VFXBuild()
    {
        BackGround_Music.instance.AudioClip(sound);
        VFXbuild_local = PoolingManager.Instance.GetPooledObject((int)VFX_Build);
        VFXbuild_local.transform.position = transform.position;
        VFXbuild_local.SetActive(true);
    }
    
    private void Deactivate_VFXBuild()
    {
        VFXbuild_local.SetActive(false);
        BackGround_Music.instance.Stopsound();

    }

    private void OnMouseDown()
    {
        DeactivateUI();

        if (isAvailable && !Producing)
        {
            if (!isUnlocked)
            {
                ActivateUI(costWoodUnlock,costStoneUnlock);
                Manager.instance.D_3();

                if (GameManager.instance.WoodPlayer >= costWoodUnlock && GameManager.instance.StonePlayer >= costStoneUnlock)
                {
                    ui_local.gameObject.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(UnlockHexagon);
                   
                    Instantiate_Able();
                    GameManager.instance.cantBuy = 0;
                }
                else
                {
                    Instantiate_Unable();
                    ++GameManager.instance.cantBuy;
                    GameManager.instance.NonResources();
                }

            }
            else if(isUnlocked && !isBuilt && canBuild)
            {
                ActivateUI(costWoodBuilt,costStoneBuilt);

                if (GameManager.instance.WoodPlayer >= costWoodBuilt && GameManager.instance.StonePlayer >= costStoneBuilt)
                {
                    ui_local.gameObject.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(BuildHexagon);
                   
                    Instantiate_Able();
                    GameManager.instance.cantBuy = 0;
                }
                else
                {
                    Instantiate_Unable();
                    ++GameManager.instance.cantBuy;
                    GameManager.instance.NonResources();
                }
            }
            else if(isUnlocked && !isBuilt && !canBuild && !isTower || isUnlocked && isBuilt && resourcesMax == 0 && !isTower)
            {
                ActivateUI(costWoodTower,costStoneTower);

                if (GameManager.instance.WoodPlayer >= costWoodTower && GameManager.instance.StonePlayer >= costStoneTower)
                {
                    ui_local.gameObject.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(BuildTower);
                   
                    Instantiate_Able();
                    GameManager.instance.cantBuy = 0;
                }
                else
                {
                    Instantiate_Unable();
                    ++GameManager.instance.cantBuy;
                    GameManager.instance.NonResources();
                }
            }
        }
    }

    private void UnlockHexagon()
    {
        Manager.instance.D_4();

        BackGround_Music.instance.AudioClip(BUYsound);
        GameManager.instance.WoodPlayer -= costWoodUnlock;
        GameManager.instance.StonePlayer -= costStoneUnlock;

        isUnlocked = true;
        
        ui_local.SetActive(false);
        able_local.SetActive(false);
        
        Instantiate_VFXBuild();
        
        Invoke("Instantiate_Unlock", 1);
        Invoke("MakeAvailableNearBiomes", 2f);
        Invoke("Deactivate_VFXBuild", 2f);
        
        GameManager.instance.RefreshUITxt();
    }
    
    private void BuildHexagon()
    {
        Manager.instance.D_6();

        BackGround_Music.instance.AudioClip(BUYsound);

        GameManager.instance.WoodPlayer -= costWoodBuilt;
        GameManager.instance.StonePlayer -= costStoneBuilt;
        GameManager.instance.TotalConstructions += 1;

        isBuilt = true;
        
        ui_local.SetActive(false);
        able_local.SetActive(false);

        Instantiate_VFXBuild();
        
        Invoke("Instantiate_Build", 1);
        Invoke("IncreaseResource", 2f);
        Invoke("Deactivate_VFXBuild", 2f);

        GameManager.instance.RefreshUITxt();
    }
    
    private void BuildTower()
    {
        BackGround_Music.instance.AudioClip(BUYsound);
        GameManager.instance.WoodPlayer -= costWoodTower;
        GameManager.instance.StonePlayer -= costStoneTower;
        GameManager.instance.TotalTowers += 1;
        LevelManager.instance.Mejora_GM();
        LevelManager.instance.Mejora_GM_2();
        
        isTower = true;

        ui_local.SetActive(false);
        able_local.SetActive(false);
        unlocked_local.SetActive(false);
        
        if (build_local != null) build_local.SetActive(false);
        
        Instantiate_VFXBuild();
        
        Invoke("Instantiate_Tower", 1);
        Invoke("Deactivate_VFXBuild", 2f);

        GameManager.instance.RefreshUITxt();
        GameManager.instance.TriggerRound();
    }
    
}

