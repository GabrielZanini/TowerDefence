using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{

    [SerializeField] private List<EnemyType> enemies = new List<EnemyType>();
    [SerializeField] private SpawnAreaManager spawnAreaManager;
    [SerializeField] private ObjectPoolsManager objectPoolsManager;
    [SerializeField] private GameplayManager gameplayManager;

    [SerializeField] private int waveNumber = 0;
    [SerializeField] private int initialEnemyNumber = 10;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float waveDelay = 15f;


    private Dictionary<EnemyType, ObjectPool> enemyPools = new Dictionary<EnemyType, ObjectPool>();

    public int WaveNumber => waveNumber;


    private int waveEnemiesCount;
    private float spawnCounter = 0f;
    private float waveCounter = 0f;

    private void Awake()
    {
        gameplayManager.OnChangeGameplayState.AddListener(GameplayStateChanged);
    }

    private void OnDestroy()
    {
        gameplayManager.OnChangeGameplayState.RemoveListener(GameplayStateChanged);
    }


    void Start()
    {
        ResetWaves();
    }

    void Update()
    {
        if (gameplayManager.GameplayState == GameplayState.Playing || gameplayManager.GameplayState == GameplayState.PlacingDefence)
        {
            PlayWaves();
        }
    }

    private void GameplayStateChanged()
    {
        if (gameplayManager.GameplayState == GameplayState.GameOver || gameplayManager.GameplayState == GameplayState.MainMenu)
        {
            ResetWaves();
        }
    }

    private void PlayWaves()
    {
        // Spawn Enemies
        if (spawnCounter <= 0f)
        {
            spawnCounter = spawnDelay;

            if (waveEnemiesCount > 0)
            {
                waveEnemiesCount -= 1;

                var enemyType = enemies[Random.Range(0, Mathf.Min(enemies.Count, waveNumber))];

                if (!enemyPools.ContainsKey(enemyType))
                {
                    enemyPools.Add(enemyType, objectPoolsManager.GetPool(enemyType));
                }

                var pool = enemyPools[enemyType];
                pool.Spawn(spawnAreaManager.GetRandomSpawnPoint(), Quaternion.Euler(0, -90, 0));
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

    public void ResetWaves()
    {
        waveEnemiesCount = initialEnemyNumber + waveNumber;
        spawnCounter = 0f;
        waveCounter = 0f;

        foreach (var pool in enemyPools)
        {
            pool.Value.DespawnAll();
        }
    }
}
