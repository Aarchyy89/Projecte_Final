using UnityEngine;

public class Cell_Detector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ship"))
        {
            other.GetComponent<Enemy_Ship_AI>().inside = true;
            other.GetComponent<Enemy_Ship_AI>().Atacar_Ayuntamiento();
        }
    }
}
