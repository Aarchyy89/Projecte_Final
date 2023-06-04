using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_Local : MonoBehaviour
{
    [SerializeField] private Animator Villager;
    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioClip soundEND;

    public void Reprocucsound()
    {
        BackGround_Music.instance.AudioClip(sound);
    }

    public void ReprocucsoundEND()
    {
        BackGround_Music.instance.AudioClip(soundEND);
    }
    public void VillagerIdle()
    {
        Villager.SetBool("Work", false);
    }
    
    public void VillagerWork()
    {
        Villager.SetBool("Work", true);
    }
}
