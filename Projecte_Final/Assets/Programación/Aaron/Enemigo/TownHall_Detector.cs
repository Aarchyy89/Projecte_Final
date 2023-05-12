using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall_Detector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ship"))
        {
            Enemy_Ship_AI.instance.Atacar_Ayuntamiento();
        }
    }


}
