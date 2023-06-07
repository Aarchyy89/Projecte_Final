using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dolly : MonoBehaviour
{
    [SerializeField] private GameObject Villager_1;

    public void VillagerWave()
    {
        Villager_1.GetComponent<Animator>().SetTrigger("Wave");
    }
    
    public void VillagerTalkTrue()
    {
        Villager_1.GetComponent<Animator>().SetBool("Talking", true);
    }
    
    public void VillagerTalkFalse()
    {
        Villager_1.GetComponent<Animator>().SetBool("Talking", false);
    }
}
