using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("----- Player Variables -----")]
    private int woodPlayer;
    private int stonePlayer;
    
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
    }
}
