using System.Collections;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class Resources_Controller : MonoBehaviour
{
    public static Resources_Controller instance;
    
    private IEnumerator currentCoroutine;
    
    public int resourcesMax;
    
    public int resourcesRound;

    public float resourcesTime;

    public int biomaType;
    
    public PoolingItemsEnum UI_Resource;

    private GameObject localUI;

    public void IncreaseResource()
    {
        //hacer animacion
        currentCoroutine = Coroutine_IncreaseResource();
        StartCoroutine(currentCoroutine);
    }
    
    private IEnumerator Coroutine_IncreaseResource()
    {
        yield return new WaitForSeconds(resourcesTime);

        ActivateUI();
    }
    
    private void ActivateUI()
    {
        localUI = PoolingManager.Instance.GetPooledObject((int)UI_Resource);
        localUI.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        
        localUI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(Harvest);
        localUI.gameObject.SetActive(true);
    }

    private void Harvest()
    {
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
        }
        
        localUI.gameObject.SetActive(false);
        PoolingManager.Instance.RemoveListener((int)UI_Resource, 1);
    }
}
