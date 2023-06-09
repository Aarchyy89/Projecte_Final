using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Resources_Controller : MonoBehaviour
{
    private IEnumerator currentCoroutine;

    public bool Producing;
    
    public int resourcesMax;
    
    public int resourcesRound;

    public float resourcesTime;

    public int biomaType;
    
    public GameObject build_local;

    public PoolingItemsEnum UI_Resource;

    private GameObject localUI;
    
    public void IncreaseResource()
    {
        currentCoroutine = Coroutine_IncreaseResource();
        StartCoroutine(currentCoroutine);
        Producing = true;
    }
    
    private IEnumerator Coroutine_IncreaseResource()
    {
        build_local.GetComponent<Animator>().SetTrigger("Next");

        yield return new WaitForSeconds(resourcesTime);
        
        ActivateUI();
    }
    
    private void ActivateUI()
    {
        localUI = PoolingManager.Instance.GetPooledObject((int)UI_Resource);
        localUI.transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        localUI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(Harvest);

        if (biomaType == 1)
        {
            localUI.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            localUI.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        }
        
        localUI.gameObject.SetActive(true);
    }

    private void Harvest()
    {
        localUI.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        localUI.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);

        switch (biomaType)
        {
            case 1:
            {
                GameManager.instance.WoodPlayer += resourcesRound;
                GameManager.instance.woodText.text = $"{GameManager.instance.WoodPlayer}";
                break;
            }
            case 2:
            {
                GameManager.instance.StonePlayer += resourcesRound;
                GameManager.instance.stoneText.text = $"{GameManager.instance.StonePlayer}";
                break;
            }
        }

        resourcesMax -= resourcesRound;
        
        if (resourcesMax > 0) 
        {
            currentCoroutine = Coroutine_IncreaseResource();
            StartCoroutine(currentCoroutine);
            Producing = true;
        }
        else
        {
            build_local.GetComponent<Animator>().SetTrigger("End");
            Producing = false;
        }
        
        localUI.gameObject.SetActive(false);
        PoolingManager.Instance.RemoveListener((int)UI_Resource, 1);
    }
}
