using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sistema_Oleadas : MonoBehaviour
{
    
    // Singleton
    public static Sistema_Oleadas Instance;
    
    [Header("----- Wave Variables -----")] 
    public List<WaveData> waveData_list;
    [SerializeField] private List<EnemyDifficultyData> enemyDifficultyData_list;

    [Tooltip("Current Wave")]
    public int waveNumber = 0;
    
    [Tooltip("Total number of enemies in the wave")]
    public int totalBoats;
    private int totalEnemies;

    public int TotalEnemies => totalEnemies;

    [Tooltip("Waiting time between one round and another")]
    [SerializeField] private float timeBetweenRounds = 10;

    [Header("----- Wave Trigger -----")]
    private IEnumerator currentCoroutine;

    private bool timerActive;
    public bool waveActive;
    public bool lastWave;

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

    private void StartRound()
    {
        StartWave();
        
        Sistema_Spawn.Instance.active_wave = true;
        Sistema_Spawn.Instance.StartSpawner();
    }
    
    void StartWave()
    {
        if (waveData_list != null && waveData_list.Count > 0)
        {
            totalBoats = waveData_list[waveNumber].TotalEnemyBoats;
            totalEnemies = waveData_list[waveNumber].TotalEnemies;
            Sistema_Spawn.Instance.current_wave = waveData_list[waveNumber];
        }
    }
    
    private bool CheckNextRound()
    {
        if (enemyDifficultyData_list[waveNumber].TotalConstructions >= GameManager.instance.TotalConstructions 
            && enemyDifficultyData_list[waveNumber].TotalTowers >= GameManager.instance.TotalTowers
            && enemyDifficultyData_list[waveNumber].LvlTownHall >= GameManager.instance.LvlTownHall
            || lastWave)
        {
            return true;
        }
        
        return false;
    }
    
    private void Checker()
    {
        if (CheckNextRound() && waveNumber != waveData_list.Count)
        {
            ++waveNumber;
        }
        
        StartRound();
    }
    
    public void BuildTrigger()
    {
        if (!waveActive)
        {
            timerActive = false;

            Checker();
            waveActive = true;
        }
    }
    
    public void TouchTrigger()
    {
        if (!waveActive && GameManager.instance.TotalTowers >= 2)
        {
            timerActive = false;
            
            currentCoroutine = TimerTriggerEnemy();
            StartCoroutine(currentCoroutine);
            waveActive = true;
            timerActive = true;
        }
    }

    IEnumerator TimerTriggerEnemy()
    {
        float timer = 0;
        Debug.Log("start round");
        
        while (timerActive)
        {
            timer += Time.deltaTime;

            if (timer >= timeBetweenRounds)
            {
                Checker();
                timerActive = false;
            }
            
            yield return null;
        }
    }
}

