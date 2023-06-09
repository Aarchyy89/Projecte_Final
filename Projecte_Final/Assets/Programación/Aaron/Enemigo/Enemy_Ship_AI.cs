using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Ship_AI : MonoBehaviour
{
    [Header("---Parameters---")]
    public NavMeshAgent navMeshAgent;
    public float range = 2f;
    
    [Header("---Fillable GO---")] 
    public PoolingItemsEnum pirate;
    public GameObject Invoked_Pirate;
    public GameObject Invoke_point;
    public GameObject[] Waypoints;
    private int Instantiated_Pirates;

    [Header("---Pirate_Timer---")]
    [SerializeField] private float time_to_spawn = 2f;
    [SerializeField] private float pirate_spawn_timer = 0f;

    public bool inside;
    public bool stopped_ship;

    public bool Hora_de_irse = false;

    private Transform WayOut;
    private IEnumerator currentCoroutine;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = 1;
        inside = false;
        stopped_ship = false;
    }

    void Update()
    {
        Checkear_Posicion_barco();

        if(Hora_de_irse)
        {
            WayOut = WP().transform;
            Me_voy();
        }
        else
        {
            Go_to_cells();
        }
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

        Instantiated_Pirates--;

        Debug.Log(Instantiated_Pirates);

        // Accedit component script varible pirates i canviarle per scriptable object

        pirateLocal.transform.position = new Vector3(Invoke_point.transform.position.x, Invoke_point.transform.position.y + 1.2f, Invoke_point.transform.position.z);
        pirateLocal.transform.rotation = Invoke_point.transform.rotation;
        pirateLocal.GetComponent<Pirate>().Vida = Sistema_Oleadas.Instance.waveData_list[Sistema_Oleadas.Instance.waveNumber].EnemyHealth;
        pirateLocal.GetComponent<Pirate>().Damage = Sistema_Oleadas.Instance.waveData_list[Sistema_Oleadas.Instance.waveNumber].EnemyAttackStats;
        pirateLocal.GetComponent<Pirate>().NavMeshAgent.speed = Sistema_Oleadas.Instance.waveData_list[Sistema_Oleadas.Instance.waveNumber].EnemySpeedStats;
        pirateLocal.gameObject.SetActive(true);

        Manager.instance.D_8();
    }

    public void Checkear_Posicion_barco()
    {
        if (stopped_ship == true)
        {
            pirate_spawn_timer += Time.deltaTime;
        }

        if (pirate_spawn_timer >= time_to_spawn)
        {
            Instantiated_Pirates = Sistema_Oleadas.Instance.TotalEnemies;
            stopped_ship = false;
            pirate_spawn_timer = 0;
            currentCoroutine = Coroutine_IncreaseResource();
            StartCoroutine(currentCoroutine);
        }
    }

    private IEnumerator Coroutine_IncreaseResource()
    {
        float timer_l = 0;
        
        while (Instantiated_Pirates > 0)
        {
            timer_l += Time.deltaTime;
            
            if (timer_l >= 2)
            {
                Llegan_los_piratas();
                timer_l = 0;
            }
            
            yield return null;
        }

        Hora_de_irse = true;
    }

    public void Me_voy()
    {
        navMeshAgent.speed = 3;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(WayOut.position);
        if(transform.position == WayOut.position)
        {
            gameObject.SetActive(false);
        }
        //Hora_de_irse = false;
    }


    private GameObject WP()
    {
        Waypoints = GameObject.FindGameObjectsWithTag("Waypoints");
        GameObject closestEnemy = null;
        float closestDistance = 0;
        bool first = true;

        foreach (var obj in Waypoints)
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

    public void Instanciar_pirata()
    {
        Instantiate(Invoked_Pirate, Invoke_point.transform.position, Invoke_point.transform.rotation);
    }
}
