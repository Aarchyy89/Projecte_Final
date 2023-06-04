using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sistema_Spawn : MonoBehaviour
{

    // Singleton
    public static Sistema_Spawn Instance;
    
    [Header("----- Spawn Variables -----")]
    public PoolingItemsEnum enemyBoat;

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
        BackGround_Music.instance.PlayPirateMusic();
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

        GameObject boat = PoolingManager.Instance.GetPooledObject((int)enemyBoat);

        // Accedit component script varible pirates i canviarle per scriptable object
        
        if (boat != null)
        {
            --sistemaOleadas.totalBoats;
            
            int spawnPoint_index = Random.Range(0, spawnPoints.Count);
            boat.transform.position = spawnPoints[spawnPoint_index].position;
            boat.gameObject.SetActive(true);
            
            spawnTimer = Random.Range(current_wave.SpawnTimer_min, current_wave.SpawnTimer_max);
        }

        timer = 0f;
        
        if (sistemaOleadas.totalBoats <= 0)
        {
            active_wave = false;
            sistemaOleadas.waveActive = false;
        }
    }
}