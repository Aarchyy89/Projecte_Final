using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Ship_AI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;

    public bool initialAnimCompleted;

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
        Debug.Log("Te estoy atacando");
        navMeshAgent.speed = 0;
        navMeshAgent.isStopped = true;
        //instanciar pirata + animacion
        //animación atacar pirata
    }

}
