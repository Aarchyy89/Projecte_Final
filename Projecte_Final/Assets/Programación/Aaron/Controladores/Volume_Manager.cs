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
        AudioListener.volume = PlayerPrefs.GetFloat("volmenMusica");
        sliderMusica.value = PlayerPrefs.GetFloat("volmenMusica");

        //sliderMusica.value = PlayerPrefs.GetFloat("volmenMusica", 0.5f);
        //sourceMusica.volume = sliderMusica.value;
        //musicaMute();
    }

    public void slidermusica(float valorMusica)
    {
        slidervalueMusica = valorMusica;
        PlayerPrefs.SetFloat("volmenMusica", slidervalueMusica);
        sourceMusica.volume = sliderMusica.value;
        //musicaMute();

    }

    public void MasterVolume()
    {
        AudioListener.volume = sliderMusica.value;
        
        PlayerPrefs.SetFloat("volmenMusica", sliderMusica.value);
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
