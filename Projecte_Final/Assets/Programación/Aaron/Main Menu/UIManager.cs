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

    public static UIManager instance;




    //PANEL MAIN MENU
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    public void Controles()
    {
        panel_controles.SetActive(true);
    }

    public void Opciones()
    {
        panel_opciones.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }


    //OPCIONES
    public void Idioma()
    {
        Idioma_panel.SetActive(true);
    }

    public void Volumen()
    {
        Volumen_panel.SetActive(true);
    }

    public void Brillo()
    {
        Brillo_panel.SetActive(true);
    }


    //RETURNS
    public void ReturnMainMenu()
    {
        panel_opciones.SetActive(false);

    }

    public void ReturnSettings()
    {
        Idioma_panel.SetActive(false);
        Volumen_panel.SetActive(false);
        Brillo_panel.SetActive(false);
        panel_controles.SetActive(false);
    }
}
