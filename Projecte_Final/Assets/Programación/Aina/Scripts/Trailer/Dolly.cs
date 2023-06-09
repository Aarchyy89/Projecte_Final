using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dolly : MonoBehaviour
{
    [SerializeField] private GameObject Villager_1;
    [SerializeField] private GameObject Resources;
    [SerializeField] private GameObject Dialogue;
    [SerializeField] private GameObject Exclamation;
    [SerializeField] private GameObject End;
    [SerializeField] private GameObject End_UI;

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
    
    public void ExclamationIn()
    {
        Exclamation.GetComponent<Animator>().SetTrigger("In");
    }
    
    public void EndIn()
    {
        End.GetComponent<Animator>().SetTrigger("In");
    }
    
    public void EndInUI()
    {
        End_UI.GetComponent<Animator>().SetTrigger("In");
    }
}
