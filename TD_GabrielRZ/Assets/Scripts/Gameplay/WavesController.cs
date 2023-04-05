using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{

    [SerializeField] private List<EnemyType> enemies = new List<EnemyType>();
    [SerializeField] private SpawnAreaManager spawnAreaManager;
    [SerializeField] private ObjectPoolsManager objectPoolsManager;

    [SerializeField] private int waveNumber = 0;
    [SerializeField] private int initialEnemyNumber = 10;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float waveDelay = 15f;



    private int waveEnemiesCount;
    private float spawnCounter = 0f;
    private float waveCounter = 0f;

    void Start()
    {
        waveEnemiesCount = initialEnemyNumber + waveNumber;
        spawnCounter = 0f;
        waveCounter = 0f;
    }

    void Update()
    {
        // Spawn Enemies
        if (spawnCounter <= 0f)
        {
            spawnCounter = spawnDelay;

            if (waveEnemiesCount > 0)
            {
                waveEnemiesCount -= 1;
                objectPoolsManager.GetPool(enemies[Random.Range(0, Mathf.Min(enemies.Count, waveNumber))]).Spawn(spawnAreaManager.GetRandomSpawnPoint(), Quaternion.identity);
            }
        }
        else
        {
            spawnCounter -= Time.deltaTime;
        }

        // Advance Wave
        if (waveCounter <= 0f)
        {
            waveEnemiesCount = initialEnemyNumber + waveNumber;
            waveCounter = waveDelay + waveNumber;
            waveNumber++;
        }
        else
        {
            waveCounter -= Time.deltaTime;
        }
    }

    public void Reset()
    {
        Start();
    }
}
