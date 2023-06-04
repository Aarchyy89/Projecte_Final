using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround_Music : MonoBehaviour
{
    public static BackGround_Music instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource Musica_fondo;
    [SerializeField] private AudioClip Backgroundmusic;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void AudioClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void Stopsound()
    {
        audioSource.Stop();
    }
}
