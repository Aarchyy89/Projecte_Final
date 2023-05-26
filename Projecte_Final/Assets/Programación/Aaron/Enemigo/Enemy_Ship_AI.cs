using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Ship_AI : MonoBehaviour
{
    [Header("---Parameters---")]
    public NavMeshAgent navMeshAgent;
    
    [Header("---Fillable GO---")] 
    public PoolingItemsEnum pirate;
    //public GameObject Invoked_Pirates;
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
        navMeshAgent.speed = 1;
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
            navMeshAgent.SetDestination(findClosestBiomaData().transform.position);
        }
    }
    
    private GameObject findClosestBiomaData() 
    {
        var data= FindObjectsOfType<Bioma_Data>();
        GameObject closestEnemy = null;
        float closestDistance = 0;
        bool first = true;
         
        foreach (var obj in data)
        {
            float distance = Vector3.Distance(obj.transform.position, transform.position);
            if (first)
            {
                closestDistance = distance;
                 
                first = false;
            }            
            else if (distance < closestDistance)
            {
                closestEnemy = obj.gameObject;
                closestDistance = distance;
            }
                                                                         
        }
        return closestEnemy;
    }

    public void Atacar_Ayuntamiento()
    {
        if(inside)
        {
            navMeshAgent.speed = 0;
            navMeshAgent.isStopped = true;
            stopped_ship = true;
        }

        //instanciar pirata + animacion
        //animaci�n atacar pirata
         
    }

    public void Llegan_los_piratas()
    {
        GameObject pirate = PoolingManager.Instance.GetPooledObject((int)this.pirate);

        // Accedit component script varible pirates i canviarle per scriptable object

        pirate.transform.position = Invoke_point.transform.position;
        pirate.transform.rotation = Invoke_point.transform.rotation;
        pirate.gameObject.SetActive(true);
        
        //Instantiate(Invoked_Pirates, Invoke_point.transform.position, Invoke_point.transform.rotation);
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
