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

    [Header("---TH MESHES----")]
    [SerializeField] private GameObject th_1;
    [SerializeField] public GameObject th_2;
    [SerializeField] public GameObject th_3;

    private int current_edif;
    public int current_Hp_edif;
    public int edif_mejora_1;
    public int edif_mejora_2;

    public bool Puedo_Mejorar;
    public bool Puedo_Mejorar_2;

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
        Town_Hall_.instance.Cambio_de_Mesh();
        Town_Hall_.instance.Cambio_de_Mesh_2();
       
        if(Puedo_Mejorar)
        {
            Town_Hall_.instance.Mejora();
            //Town_Hall_.instance.TH_HP = Town_Hall_.instance.TH_HP + Town_Hall_.instance.hp_amount;
            Debug.Log("Hey");
            Puedo_Mejorar = false;
        }

        if(Puedo_Mejorar_2)
        {
            Town_Hall_.instance.Mejora_2();
            Puedo_Mejorar_2 = false;
            Debug.Log("false");
        }
    }

    public void Recuento_Edif()
    {
        current_edif = edificios.Count; 
    }

    //FUNCION QUE COMPRUEBA CADA VEZ QUE CONSTRUYES SI SE CUMPLE EL REQUISITO PARA MEJORAR A FASE 1
    public void Mejora_1()
    {
        if (current_edif >= edif_mejora_1)
        {
            Puedo_Mejorar = true;
        }
    }

    public void Mejora_2()
    {
        if (current_edif >= edif_mejora_2)
        {
            Puedo_Mejorar_2 = true;
        }
    }
}
