using UnityEngine;
using UnityEngine.UI;

public class Town_Hall_ : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public int TH_HP;
    [SerializeField] private int damage_amount; //DAÑO QUE RECIBE 
    [SerializeField] public int hp_amount; //vida QUE recibe al mejorar

    //STATS SISTEMA DE MEJORA
    //SE UPGRADEA CADA X EDIFICIOS CONSTRUIDOS -> SUBIDA DE STATS 

    [Header("---fillable---")]
    //[SerializeField] private Slider health_bar;
    [SerializeField] private GameObject VFX_mEJORA;
    [SerializeField] private Slider Health_Slider;

    [Header("---Cambio Mesh Timer---")]
    [SerializeField] private float time_to_swapmesh = 3f;
    [SerializeField] private float TH_Mesh_timer = 0f;

    public bool mejorado;
    public bool mejorado_2;

    public static Town_Hall_ instance;

    //----LOGICAS----

    //TAKE_DAMAGE

    //LOGICA MUERTE

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
    
    private void Start()
    {
        TH_HP = 70;
        Health_Slider.value = TH_HP;
    }

    private void OnEnable()
    {
        TH_HP = PlayerPrefs.GetInt("TH_HP", TH_HP);
    }

    public void Mejora()
    {
        //mejoro vida
        TH_HP = TH_HP + hp_amount;
        Health_Slider.value = TH_HP;
        PlayerPrefs.SetInt("TH_HP", TH_HP);

        //llamo vfx
        VFX_mEJORA.SetActive(true);
        
        //Destruyo el gameObjt
        Invoke("DeactivateVFX", 3f);
        Destroy(gameObject, 2.2f);
        mejorado = true;
    }
    
    public void Mejora_2()
    {
        //mejoro vida
        TH_HP = TH_HP + hp_amount;
        Health_Slider.value = TH_HP;

        //llamo vfx
        VFX_mEJORA.SetActive(true);

        //Destruyo el gameObjt
        Invoke("DeactivateVFX", 3f);
        Destroy(gameObject, 2.2f);
        mejorado_2 = true;
    }

    private void DeactivateVFX()
    {
        VFX_mEJORA.SetActive(false);
    }

    public void Cambio_de_Mesh()
    {
        if (mejorado == true)
        {
            TH_Mesh_timer += Time.deltaTime;
        }

        if (TH_Mesh_timer >= time_to_swapmesh)
        {
            LevelManager.instance.th_2.SetActive(true);
            mejorado = false;
            TH_Mesh_timer = 0;
        }
    }

    public void Cambio_de_Mesh_2()
    {
        if (mejorado_2 == true)
        {
            TH_Mesh_timer += Time.deltaTime;
        }

        if (TH_Mesh_timer >= time_to_swapmesh)
        {
            LevelManager.instance.th_3.SetActive(true);
            mejorado_2 = false;
            TH_Mesh_timer = 0;
            GameManager.instance.LastRound();
        }
    }

    public void TakeDamage(int damage)
    {
        TH_HP -= damage;
        Health_Slider.value = TH_HP;

        if (TH_HP <= 0)
        {
            GameManager.instance.LoseCheck();
        }
    }
}
