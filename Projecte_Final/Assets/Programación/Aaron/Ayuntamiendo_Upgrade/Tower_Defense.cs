using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Defense : MonoBehaviour
{

    [Header("Attributes")]
    public float range;
    public float turret_speed;
    public float fire_Rate = 1f;
    private float fire_Countdown = 0f;

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
