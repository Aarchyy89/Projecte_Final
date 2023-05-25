using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        woodPlayer = 100;
        stonePlayer = 100;
        RefreshUITxt();
    }

    public void RefreshUITxt()
    {
        woodText.text = $"{woodPlayer}";
        stoneText.text = $"{stonePlayer}";
    }

    public void WinCheck()
    {
        
    }

    public void LoseCheck()
    {
    }
}
