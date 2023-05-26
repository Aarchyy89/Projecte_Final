using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TH_detector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pirate"))
        {
            other.GetComponent<Pirate>().inside = true;
            other.GetComponent<Pirate>().Atacar_Ayuntamiento();
        }
    }
}
