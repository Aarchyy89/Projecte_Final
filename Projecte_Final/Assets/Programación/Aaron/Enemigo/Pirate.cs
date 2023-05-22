using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class Pirate : MonoBehaviour
{
    [Header("---Parameters---")]
    private NavMeshAgent navMeshAgent;
    public Transform ayuntamiento;
    //[SerializeField] private Animator anim;

    [Header("Stats")]
    [SerializeField] private int HP;
    [SerializeField] private int max_HP;
    [SerializeField] private int damage_amount;

    [Header("---Death_Timer---")]
    [SerializeField] private float time_to_die = 2f;
    [SerializeField] private float die_timer = 0f;

    public bool inside;
    public bool player_dead;

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
        ayuntamiento = GameObject.FindGameObjectWithTag("TH").transform;
        inside = false;
        player_dead = false;
    }

    private void Update()
    {
        Go_TH();
        Pirate_Death();
        if (Input.GetKeyDown(KeyCode.P))
        {
            TAKE_damage();
        }
    }

    public void Go_TH()
    {
        if (navMeshAgent.speed >= 0.1f)
        {
            //anim.SetBool("Walk", true);
            navMeshAgent.SetDestination(ayuntamiento.position);
        }
    }

    public void Atacar_Ayuntamiento()
    {
        if (inside == true)
        {
            navMeshAgent.speed = 0;
            navMeshAgent.isStopped = true;
            //atacar animacion
            //llamar al script del ayuntamiento y aplicar la funcion de quitar vida 
        }

    }

    public void TAKE_damage()
    {
        HP -= damage_amount;
        //Health_bar.value = HP;


        if (HP <= 0)
        {
            player_dead = true;
            //animator.SetTrigger("Die");
            
        }
    }

    private void Pirate_Death()
    {
        if (player_dead)
        {
            die_timer += Time.deltaTime;
        }

        if (die_timer >= time_to_die)
        {
            Destroy(gameObject);
            player_dead = false;
            die_timer = 0;
        }


    }

}
