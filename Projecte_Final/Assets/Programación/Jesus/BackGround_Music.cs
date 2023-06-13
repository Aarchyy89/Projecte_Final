using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround_Music : MonoBehaviour
{
    public static BackGround_Music instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource Musica_fondo;

    [SerializeField] private List<AudioClip> BackgroundMusic_played;
    [SerializeField] private List<AudioClip> BackgroundMusic_toplay;
    [SerializeField] private List<AudioClip> PirateMusic;
    private bool pirateTime;

    private float delay;
    
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

        BackgroundSound();
    }

    public void AudioClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void Stopsound()
    {
        audioSource.Stop();
    }

    private void BackgroundSound()
    {
        if (pirateTime)
        {
            Musica_fondo.PlayOneShot(BackgroundMusic());
            
            Invoke("BackgroundSound", delay);
        }
    }

    private AudioClip BackgroundMusic()
    {
        int index = Random.Range(0, BackgroundMusic_toplay.Count);

        if (BackgroundMusic_toplay.Count == 0)
        {
            BackgroundMusic_toplay.AddRange(BackgroundMusic_played);
            BackgroundMusic_played.Clear();
        }
        
        AudioClip sound = BackgroundMusic_toplay[index];

        foreach (var listSound in BackgroundMusic_toplay)
        {
            if (listSound.name == sound.name)
            {
                BackgroundMusic_played.Add(listSound);
                BackgroundMusic_toplay.Remove(listSound);
                break;
            }
        }

        delay = sound.length;
        
        return sound;
    }

    public void PlayPirateMusic()
    {
        pirateTime = true;
        
        Musica_fondo.Stop();
        Musica_fondo.PlayOneShot(PirateMusic_());

        Invoke("PirateFalse", delay - 0.5f);
        Invoke("BackgroundSound", delay);
    }

    private void PirateFalse()
    {
        pirateTime = false;
    }
    
    private AudioClip PirateMusic_()
    {
        int index = Random.Range(0, PirateMusic.Count);

        AudioClip sound = PirateMusic[index];
        
        delay = sound.length;
        
        return sound;
    }
}
