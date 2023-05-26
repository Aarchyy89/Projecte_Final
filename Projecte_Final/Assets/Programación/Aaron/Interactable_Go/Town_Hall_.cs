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

    [Header("---fillable---")]
    //[SerializeField] private Slider health_bar;
    [SerializeField] private GameObject VFX_mEJORA;

    [Header("---Cambio Mesh Timer---")]
    [SerializeField] private float time_to_swapmesh = 3f;
    [SerializeField] private float TH_Mesh_timer = 0f;

    public bool jejeje;

    public static Town_Hall_ instance;

    //----LOGICAS----

    //TAKE_DAMAGE

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
        //mejoro vida
        TH_HP = TH_HP + hp_amount;

        //llamo vfx
        VFX_mEJORA.SetActive(true);
        
        //Destruyo el gameObjt
        Destroy(VFX_mEJORA, 3f);
        Destroy(gameObject, 2.2f);
        jejeje = true;
    }

    public void Cambio_de_Mesh()
    {
        if (jejeje == true)
        {
            TH_Mesh_timer += Time.deltaTime;
        }

        if (TH_Mesh_timer >= time_to_swapmesh)
        {
            LevelManager.instance.th_2.SetActive(true);
            jejeje = false;
            TH_Mesh_timer = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        TH_HP -= damage;

        if (TH_HP <= 0)
        {
            GameManager.instance.LoseCheck();
        }
    }
}
