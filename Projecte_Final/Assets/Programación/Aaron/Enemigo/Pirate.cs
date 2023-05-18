using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class Pirate : MonoBehaviour
{
    [Header("---Parameters---")]
    public NavMeshAgent navMeshAgent;
    public Transform Parcela;

    public bool inside;

    public static Pirate instance;

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

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Celdas").transform;
    }

    public void Move_to_Cells()
    {
        if (navMeshAgent.speed >= 0.1f)
        {
            //anim.SetBool("Walk", true);
            navMeshAgent.SetDestination(Parcela.position);
        }
    }

    public void Atacar_Parcela()
    {
        if (inside == true)
        {
            navMeshAgent.speed = 0;
            navMeshAgent.isStopped = true;
            //stopped_ship = true;
        }

        //instanciar pirata + animacion
        //animación atacar pirata

    }
}
