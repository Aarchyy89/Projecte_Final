using System;
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
    public GameObject Pausepanel;
    private bool pauseBool;
    public static UIManager instance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Level_Merge")
        {
            if (!pauseBool)
            {
                Pausepanel.SetActive(true);
                pauseBool = true;
                Time.timeScale = 0;
            }
        }
    }

    public void ResumeGame()
    {
        Pausepanel.SetActive(false);
        pauseBool = false;
        Time.timeScale = 1;
    }

    //PANEL MAIN MENU
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
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
