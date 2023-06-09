using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dolly : MonoBehaviour
{
    [SerializeField] private GameObject Villager_1;
    [SerializeField] private GameObject Resources;
    [SerializeField] private GameObject Dialogue;

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
    
    public void DialogueIn()
    {
        Dialogue.GetComponent<Animator>().SetTrigger("In");
    }
    
    public void DialogueTxt()
    {
        Dialogue.GetComponent<Animator>().SetTrigger("Next");
    }
    
    public void ResourcesIn()
    {
        Resources.GetComponent<Animator>().SetTrigger("In");
    }
}
