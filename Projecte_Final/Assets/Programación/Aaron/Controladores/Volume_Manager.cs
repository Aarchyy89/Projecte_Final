using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Volume_Manager : MonoBehaviour
{
    [Header("musica")] 
    public Slider sliderMusica;
    public float slidervalueMusica;
    public AudioSource sourceMusica;


    void Start()
    {
        sliderMusica.value = PlayerPrefs.GetFloat("volmenMusica", 0.5f);
        sourceMusica.volume = sliderMusica.value;
        //musicaMute();
    }

    public void slidermusica(float valorMusica)
    {
        slidervalueMusica = valorMusica;
        PlayerPrefs.SetFloat("volmenMusica", slidervalueMusica);
        sourceMusica.volume = sliderMusica.value;
        //musicaMute();

    }

    /*public void musicaMute()
    {
        if (slidervalueMusica == 0)
        {
            imgMuteMusica.enabled = true;
        }
        else
        {
            imgMuteMusica.enabled = false;
        }
    }*/
}
