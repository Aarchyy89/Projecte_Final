using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resources_Controller : MonoBehaviour
{
    public static Resources_Controller instance;

    private void Awake()
    {
        instance = this;    
    }

    public IEnumerator SubirMadera()
    {
        yield return new WaitForSeconds(2f);

        LevelManager.instance.current_wood += 15;
        LevelManager.instance.text_wood.text = LevelManager.instance.current_wood + "";
    }

    public void IncreaseMadera()
    {
        if(Input.GetKeyDown(KeyCode.P))
            
        {
            //hacer animacion
            StartCoroutine(SubirMadera());

        }
    }


}
