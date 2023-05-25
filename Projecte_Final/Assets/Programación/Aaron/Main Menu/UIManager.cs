using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("---Paneles---")]
    public GameObject panel_controles;
    public GameObject panel_opciones;
    public GameObject Idioma_panel;
    public GameObject Volumen_panel;
    public GameObject Brillo_panel;
    public GameObject Mainpanel;
    public static UIManager instance;




    //PANEL MAIN MENU
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    public void Controles()
    {
        panel_controles.SetActive(true);
        Mainpanel.SetActive(false);

    }

    public void Opciones()
    {
        panel_opciones.SetActive(true);
        Mainpanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }


    //OPCIONES
    public void Idioma()
    {
        Idioma_panel.SetActive(true);
        Mainpanel.SetActive(false);
        panel_opciones.SetActive(false);
    }

    public void Volumen()
    {
        Volumen_panel.SetActive(true);
    }

    public void Brillo()
    {
        Brillo_panel.SetActive(true);
    }
    
    public void volvermenuopciones()
    {
        Idioma_panel.SetActive(false);
        Mainpanel.SetActive(false);
        panel_opciones.SetActive(true);


    }

    //RETURNS
    public void ReturnMainMenu()
    {
        panel_opciones.SetActive(false);
        Mainpanel.SetActive(true);
        panel_controles.SetActive(false);
    }

    public void ReturnSettings()
    {
        Idioma_panel.SetActive(false);
        Volumen_panel.SetActive(false);
        Brillo_panel.SetActive(false);
        panel_controles.SetActive(false);
    }
}
