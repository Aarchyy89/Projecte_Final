using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Defense : MonoBehaviour
{
    public Transform pirate_target;
    public Transform part_Torotate;
    //[SerializeField] private AudioClip Shoot_clip;

    [Header("Attributes")]
    public float range;
    public float turret_speed;
    public float fire_Rate = 1f;
    private float fire_Countdown = 0f;

    [Header("---fillable GO---")]
    public GameObject bullet;
    public Transform Shoot_Point;
    
    public string piratetag = "Pirate";


    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(piratetag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {

            float distanceToPlayer = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToPlayer < shortestDistance)
            {
                shortestDistance = distanceToPlayer;
                nearestEnemy = enemy;

            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            pirate_target = nearestEnemy.transform;
        }
        else
        {
            pirate_target = null;
        }
    }

    

    private void Update()
    {
        if (pirate_target == null)
        {
            return;
        }

        //rotacion de la torreta al jugador 
        Vector3 dir = pirate_target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(part_Torotate.rotation, lookRotation, Time.deltaTime * turret_speed).eulerAngles;
        part_Torotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fire_Countdown <= 0f)
        {
            Shoot();
            fire_Countdown = 1f / fire_Rate;
        }

        fire_Countdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bullet, Shoot_Point.position, Shoot_Point.rotation);
        Bala_Escopeta buullet = bulletGO.GetComponent<Bala_Escopeta>();

        if(buullet != null)
        {
            buullet.Seek(pirate_target);
        }

        /**Audio_Manager.instance.AudioClip(Shoot_clip);

        if (buullet != null)
        {
            buullet.Seek(target);
        }*/

    }
}
