using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Ship_AI : MonoBehaviour
{
    [Header("---Parameters---")]
    public NavMeshAgent navMeshAgent;
    public Transform Parcela;

    [Header("---Fillable GO---")]
    public GameObject Invoked_Pirates;
    public GameObject Invoke_point;

    [Header("---Pirate_Timer---")]
    [SerializeField] private float time_to_spawn = 2f;
    [SerializeField] private float pirate_spawn_timer = 0f;

    public bool inside;
    public bool stopped_ship;

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
        Parcela = GameObject.FindGameObjectWithTag("Parcela").transform;
        inside = false;
        stopped_ship = false;
    }

    void Update()
    {

        Go_to_cells();
        Checkear_Posicion_barco();

    }

    public void Go_to_cells()
    {
        if (navMeshAgent.speed >= 0.1f)
        {
            //anim.SetBool("Walk", true);
            navMeshAgent.SetDestination(Parcela.position);
        }
    }

    public void Atacar_Ayuntamiento()
    {
        if(inside == true)
        {
            navMeshAgent.speed = 0;
            navMeshAgent.isStopped = true;
            stopped_ship = true;
        }

        //instanciar pirata + animacion
        //animación atacar pirata
         
    }

    public void Llegan_los_piratas()
    {
        Instantiate(Invoked_Pirates, Invoke_point.transform.position, Invoke_point.transform.rotation);
    }

    public void Checkear_Posicion_barco()
    {
        if (stopped_ship == true)
        {
            pirate_spawn_timer += Time.deltaTime;
        }

        if (pirate_spawn_timer >= time_to_spawn)
        {
            Llegan_los_piratas();
            stopped_ship = false;
            pirate_spawn_timer = 0;
        }
    }

}
