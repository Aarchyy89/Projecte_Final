using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sistema_Spawn : MonoBehaviour
{

    // Singleton
    public static Sistema_Spawn Instance;
    
    [Header("----- Spawn Variables -----")]
    public PoolingItemsEnum enemyType;

    public WaveData current_wave;
    
    public List<Transform> spawnPoints;
    
    private IEnumerator currentCoroutine;

    public bool active_wave;
    private float spawnTimer = 0.1f;
    private float timer = 0f;

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


    public void StartSpawner()
    {
        currentCoroutine = Coroutine_StartSpawner();
        StartCoroutine(currentCoroutine);
    }

    IEnumerator Coroutine_StartSpawner()
    {
        spawnTimer = Random.Range(current_wave.SpawnTimer_min, current_wave.SpawnTimer_max);
        
        while (active_wave)
        {
            timer += Time.deltaTime;

            if(timer >= spawnTimer)
            {
                Spawner();
            }
            
            yield return null;
        }
    }

    void Spawner()
    {
        Sistema_Oleadas sistemaOleadas = Sistema_Oleadas.Instance;

        if (sistemaOleadas.CheckEndRound())
        {
            return;
        }
        
        GameObject enemy = PoolingManager.Instance.GetPooledObject((int)enemyType);

        if (enemy != null)
        {
            --sistemaOleadas.TotalEnemies;
            
            // Get random spawn point from the list
            int spawnPoint_index = Random.Range(0, spawnPoints.Count);
            enemy.transform.position = spawnPoints[spawnPoint_index].position;
            enemy.gameObject.SetActive(true);
            
            spawnTimer = Random.Range(current_wave.SpawnTimer_min, current_wave.SpawnTimer_max);
        }

        if (sistemaOleadas.waveNumber == 14 && Sistema_Oleadas.Instance.TotalEnemies >= 50)
        {
            // Spawn Boss
        }

        timer = 0f;
        sistemaOleadas.Checker();
    }
}