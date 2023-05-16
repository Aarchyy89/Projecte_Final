using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Ship_AI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;

    private bool initialAnimCompleted;
    public bool inside;

    public static Enemy_Ship_AI instance;

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

        //anim = GetComponent<Animator>();
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("TH").transform;
        inside = false;
    }

    void Update()
    {

        ChasePlayer();
        
    }

    public void ChasePlayer()
    {
        if (navMeshAgent.speed >= 0.1f)
        {
            //anim.SetBool("Walk", true);
            navMeshAgent.SetDestination(player.position);
        }
    }

    public void Atacar_Ayuntamiento()
    {
        if(inside == true)
        {
            navMeshAgent.speed = 0;
            navMeshAgent.isStopped = true;
        }
        Debug.Log("Hola");
        //instanciar pirata + animacion
        //animación atacar pirata
    }

}
