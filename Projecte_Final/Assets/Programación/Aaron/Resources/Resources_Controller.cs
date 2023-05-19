using System.Collections;
using UnityEngine;

public class Resources_Controller : MonoBehaviour
{
    public static Resources_Controller instance;
    
    private IEnumerator currentCoroutine;
    
    public int resourcesMax;
    
    public int resourcesRound;

    public float resourcesTime;

    public int biomaType;
    
    public PoolingItemsEnum UI_Resource;

    public void IncreaseResource()
    {
        //hacer animacion
        currentCoroutine = Coroutine_IncreaseResource();
        StartCoroutine(currentCoroutine);
    }
    
    private IEnumerator Coroutine_IncreaseResource()
    {
        yield return new WaitForSeconds(resourcesTime);

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
            StartCoroutine(currentCoroutine);
        }
    }
}
