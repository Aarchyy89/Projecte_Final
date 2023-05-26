using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class Enemy_Ship_AI : MonoBehaviour
{
    [Header("---Parameters---")]
    public NavMeshAgent navMeshAgent;
    
    [Header("---Fillable GO---")] 
    public PoolingItemsEnum pirate;
    //public GameObject Invoked_Pirates;
    public GameObject Invoke_point;
    public List<Transform> Waypoints = new List<Transform>();

    [Header("---Pirate_Timer---")]
    [SerializeField] private float time_to_spawn = 2f;
    [SerializeField] private float pirate_spawn_timer = 0f;

    public bool inside;
    public bool stopped_ship;

    public bool Hora_de_irse = false;

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
        Me_voy();
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
        GameObject pirateLocal = PoolingManager.Instance.GetPooledObject((int)pirate);

        // Accedit component script varible pirates i canviarle per scriptable object

        pirateLocal.transform.position = Invoke_point.transform.position;
        pirateLocal.transform.rotation = Invoke_point.transform.rotation;
        pirateLocal.gameObject.SetActive(true);
        
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
            Hora_de_irse = true;
        }

    }

    public void Me_voy()
    {
        if(Hora_de_irse)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Waypoints");
            foreach (Transform t in go.transform)
            {
                Waypoints.Add(t);
            }
            navMeshAgent.SetDestination(Waypoints[Random.Range(0, Waypoints.Count)].position);
            Debug.Log("Hey");
            Hora_de_irse = false;
        }
    }

}
