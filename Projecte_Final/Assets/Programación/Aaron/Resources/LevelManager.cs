using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> edificios;

    [Header("Fillable")]
    public GameObject edif;
    public GameObject punto_instantiate_edif;

    private int current_edif;
    public int current_Hp_edif;
    public int edif_mejora_1;

    public bool Puedo_Mejorar;

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        edificios = new List<GameObject>();
    }

    private void Update()
    {
        //Resources_Controller.instance.IncreaseMadera();
        current_Hp_edif = Town_Hall_.instance.TH_HP;

        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(edif, punto_instantiate_edif.transform.position, punto_instantiate_edif.transform.rotation);
            edificios.Add(edif);
        }

        Recuento_Edif();
        //Mejora_1();
       
        if(Puedo_Mejorar)
        {
            Town_Hall_.instance.Mejora();
            Puedo_Mejorar = false;
            Debug.Log("false");
        }


    }

    public void Recuento_Edif()
    {
        current_edif = edificios.Count; 
    }

    public void Mejora_1()
    {
        if (current_edif >= edif_mejora_1)
        {
            Puedo_Mejorar = true;
            Debug.Log("true");
        }
    }



}
