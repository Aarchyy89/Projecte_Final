using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("----- Player Variables -----")]
    private int woodPlayer;
    private int stonePlayer;
    
    private int totalConstructions;
    private int totalTowers;
    private int lvlTownHall;

    [Header("----- UI Variables -----")] 
    public TMP_Text woodText;
    public TMP_Text stoneText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
        
    public int WoodPlayer
    {
        get { return woodPlayer; }
        set { woodPlayer = value; }
    }    
    
    public int StonePlayer
    {
        get { return stonePlayer; }
        set { stonePlayer = value; }
    }
    
    public int TotalConstructions
    {
        get { return totalConstructions; }
        set { totalConstructions = value; }
    }    
    
    public int TotalTowers
    {
        get { return totalTowers; }
        set { totalTowers = value; }
    }
    
    public int LvlTownHall
    {
        get { return lvlTownHall; }
        set { lvlTownHall = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        woodPlayer = 20;
        stonePlayer = 20;
        RefreshUITxt();
    }

    public void RefreshUITxt()
    {
        woodText.text = $"{woodPlayer}";
        stoneText.text = $"{stonePlayer}";
    }

    // Pasar a millor d'ultim nivell
    public void LastRound()
    {
        Sistema_Oleadas.Instance.lastWave = true;
        Sistema_Oleadas.Instance.waveActive = false;
        Sistema_Oleadas.Instance.BuildTrigger();
    }

    public void WinCheck()
    {
        winPanel.SetActive(true);
    }

    public void LoseCheck()
    {
        losePanel.SetActive(true);
    }
}
