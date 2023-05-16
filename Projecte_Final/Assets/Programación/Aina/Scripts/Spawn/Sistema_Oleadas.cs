using System;
using System.Collections.Generic;
using UnityEngine;

public class Sistema_Oleadas : MonoBehaviour
{
    
    // Singleton
    public static Sistema_Oleadas Instance;
    
    [Header("----- Variables -----")] 
    public List<WaveData> waveData_list;

    [Tooltip("Current Wave")]
    public int waveNumber = 0;
    
    [Tooltip("Total number of enemies in the wave")]
    public int totalEnemies;
    
    [Tooltip("Number of enemies the player has eliminated")]
    private int defeatedEnemies;
    
    [Tooltip("Waiting time between one round and another")]
    private float timeBetweenRounds = 30;
    
    
    
    public bool CheckEndRound()
    {
        if (totalEnemies <= 0)
        {
            return true;
        }
        
        return false;
    }
    
    public int TotalEnemies
    {
        get {return totalEnemies; }
        set {totalEnemies = value; }
    }
    
    public int DefeatedEnemies
    {
        get {return defeatedEnemies; }
        set {defeatedEnemies = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartRound();
    }

    public void StartRound()
    {
        StartWave();
        
        Sistema_Spawn.Instance.active_wave = true;
        Sistema_Spawn.Instance.StartSpawner();
    }
    
    void StartWave()
    {
        if (waveData_list != null && waveData_list.Count > 0)
        {
            totalEnemies = waveData_list[waveNumber].TotalEnemies;
            Sistema_Spawn.Instance.current_wave = waveData_list[waveNumber];
        }
    }
    
    public void Checker()
    {
        if (CheckEndRound() && waveNumber != waveData_list.Count)
        {
            ++waveNumber; //Increment by 1. Same as:  waveNumber = waveNumber + 1;
            // Void activate menu upgrade
            StartRound();
        }
    }
}

