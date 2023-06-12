using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Pirate : MonoBehaviour
{
    [Header("--- Parameters ---")]
    private NavMeshAgent navMeshAgent;
    public Transform ayuntamiento;
    [SerializeField] private Animator anim;
    //[SerializeField] private Animator anim;
    public PoolingItemsEnum death_vfx;
    public PoolingItemsEnum attack_vfx;
    private IEnumerator currentCoroutine;

    [Header("--- Stats ---")]
    private int HP;
    private int Attack_damage;

    public bool inside;
    public bool player_dead;
    private GameObject activeTownHall;

    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioClip[] muertesound;


    public void espadasound()
    {
        BackGround_Music.instance.AudioClip(sound);
    }
    
    public int Vida
    {
        get { return HP; }
        set { HP = value; }
    }
    
    public int Damage
    {
        get { return Attack_damage; }
        set { Attack_damage = value; }
    }
    
    public NavMeshAgent NavMeshAgent
    {
        get { return navMeshAgent; }
        set { navMeshAgent = value; }
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
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
        GameObject attackVFX = PoolingManager.Instance.GetPooledObject((int)attack_vfx);
        GameObject point = gameObject.transform.GetChild(1).gameObject;
        attackVFX.transform.position = point.transform.position;
        attackVFX.transform.rotation = point.transform.rotation;
        attackVFX.gameObject.SetActive(true);
        activeTownHall = GameObject.FindGameObjectWithTag("TH");
        activeTownHall.GetComponent<Town_Hall_>().TakeDamage(Attack_damage);
    }

    public void TAKE_damage(int damage)
    {
        HP -= damage;
        //Health_bar.value = HP;

        if (HP <= 0 && !player_dead)
        {
            player_dead = true;
            navMeshAgent.speed = 0;
            anim.SetTrigger("Die");
            BackGround_Music.instance.AudioClip(muertesound[0]);
            currentCoroutine = Coroutine_PirateDeath();
            StartCoroutine(currentCoroutine);
        }
        
        GameManager.instance.WinCheck();
    }

    private IEnumerator Coroutine_PirateDeath()
    {
        yield return new WaitForSeconds(1.5f);
        
        GameObject deathVFX = PoolingManager.Instance.GetPooledObject((int)death_vfx);
        deathVFX.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.25f, gameObject.transform.position.z);
        deathVFX.transform.rotation = gameObject.transform.rotation;
        deathVFX.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
        
        if (Sistema_Oleadas.Instance.lastWave)
        {
            --GameManager.instance._LastRoundEnemies;
        }
        gameObject.SetActive(false);
    }
}
