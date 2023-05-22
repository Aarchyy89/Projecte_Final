using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Town_Hall_ : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public int TH_HP;
    [SerializeField] private int damage_amount; //DAÑO QUE RECIBE 
    [SerializeField] private int hp_amount; //vida QUE recibe al mejorar

    //STATS SISTEMA DE MEJORA
    //SE UPGRADEA CADA X EDIFICIOS CONSTRUIDOS -> SUBIDA DE STATS 

    //[Header("---fillable---")]
    //[SerializeField] private Slider health_bar;

    public static Town_Hall_ instance;

    //----LOGICAS----

    //TAKE_DAMAGE

    //LOGICA MEJORA

    //LOGICA MUERTE

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }


    public void Mejora()
    {
        TH_HP = TH_HP + hp_amount;
        //---LLAMAR POR EVENTO---
        //llamar vfx
        //cambiar gameobject
    }



}
