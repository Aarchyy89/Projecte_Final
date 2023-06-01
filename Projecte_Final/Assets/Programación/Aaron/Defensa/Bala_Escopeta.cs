using UnityEngine;

public class Bala_Escopeta : MonoBehaviour
{
    private Pirate pirate_target;

    public float speed = 70f;
    [SerializeField] private int damage;

    public void Seek(Pirate _target)
    {
        pirate_target = _target;
        Invoke("Lifetime", 5);
    }

    private void Update()
    {
        if (pirate_target == null && !pirate_target.player_dead)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 dir = pirate_target.gameObject.transform.position - transform.position;
        float distance_This = speed * Time.deltaTime;

        if (dir.magnitude <= distance_This)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distance_This, Space.World);
    }

    private void HitTarget()
    {
        pirate_target.TAKE_damage(damage);
        gameObject.SetActive(false);

        if (Sistema_Oleadas.Instance.lastWave)
        {
            --GameManager.instance._LastRoundEnemies;
        }
    }
    
    private void Lifetime()
    {
        pirate_target.TAKE_damage(damage);
        gameObject.SetActive(false);

        if (Sistema_Oleadas.Instance.lastWave)
        {
            --GameManager.instance._LastRoundEnemies;
        }
    }
}
