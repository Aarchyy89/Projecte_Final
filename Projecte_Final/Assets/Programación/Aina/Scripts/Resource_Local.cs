using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_Local : MonoBehaviour
{
    [SerializeField] private Animator Villager;

    public void VillagerIdle()
    {
        Villager.SetBool("Work", false);
    }
    
    public void VillagerWork()
    {
        Villager.SetBool("Work", true);
    }
}
