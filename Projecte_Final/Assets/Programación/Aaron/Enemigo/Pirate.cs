using UnityEngine;
using UnityEngine.AI;

public class Pirate : MonoBehaviour
{
    [Header("--- Parameters ---")]
    private NavMeshAgent navMeshAgent;
    public Transform ayuntamiento;
    [SerializeField] private Animator anim;
    //[SerializeField] private Animator anim;

    [Header("--- Stats ---")]
    [SerializeField] private int HP;
    [SerializeField] private int damage_amount;
    [SerializeField] private int Attack_damage;

    [Header("--- Death Timer ---")]
    [SerializeField] private float time_to_die = 2f;
    [SerializeField] private float die_timer = 0f;

    public bool inside;
    public bool player_dead;

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
            anim.SetBool("Attack", true);
            //llamar al script del ayuntamiento y aplicar la funcion de quitar vida
        }
        else
        {
            anim.SetBool("Attack", false);
        }

    }

    public void Make_Damage()
    {
        Town_Hall_.instance.TakeDamage(Attack_damage);
    }

    public void TAKE_damage(int damage)
    {
        HP -= damage;
        //Health_bar.value = HP;

        if (HP <= 0)
        {
            player_dead = true;
            navMeshAgent.speed = 0;
            anim.SetTrigger("Die");
            Invoke("Pirate_Death", 2);
        }
    }

    private void Pirate_Death()
    {
        gameObject.SetActive(false);
    }
}
