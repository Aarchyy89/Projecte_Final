using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TH_detector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pirate"))
        {
            Pirate.instance.inside = true;
            Pirate.instance.Atacar_Ayuntamiento();
        }
    }
}
