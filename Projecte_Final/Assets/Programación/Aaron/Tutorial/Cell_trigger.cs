using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ship"))
        {
            other.GetComponent<Enemy_Ship_AI>().Instanciar_pirata();
        }
    }
}
