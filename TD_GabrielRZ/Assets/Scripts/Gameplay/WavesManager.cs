using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private WaveSettings waveSettings;
    [SerializeField] private SpawnAreaManager spawnAreaManager;
    [SerializeField] private ObjectPoolsManager objectPoolsManager;

    [ShowNativeProperty]
    public int WaveNumber { get; protected set; }

    [ShowNativeProperty]
    public int SubWaveNumber { get; protected set; }

    private Wave Wave { get { return waveSettings.waves[WaveNumber]; } }
    private SubWave SubWave { get { return waveSettings.waves[WaveNumber].subwaves[SubWaveNumber]; } }
    private int WaveNumberLoop { get { return loopLastWave ? waveSettings.waves.Count - 1 : WaveNumber; } }

    private List<EnemyType> subWaveEnemies = new List<EnemyType>();
    private Dictionary<EnemyType, ObjectPool> enemyPools = new Dictionary<EnemyType, ObjectPool>();
    [SerializeField] private bool loopLastWave = false;
    [SerializeField] private float enemyCooldown = 0f;

    private void Start()
    {
        WaveNumber = 0;
        SubWaveNumber = 0;
        StartWave();
    }

    void Update()
    {
        if (IsSubWaveOver())
        {
            SubWaveNumber += 1;

            if (IsWaveOver())
            {
                WaveNumber += 1;
                SubWaveNumber = 0;

                if (AreAllWavesOver())
                {
                    loopLastWave = true;
                }

                StartWave();
            }
            else
            {                
                StartSubWave();
            }
        }
        else
        {
            SpawnRandomEnemy();
        }

    }

    private void SpawnRandomEnemy()
    {
        if (enemyCooldown <= 0f && subWaveEnemies.Count > 0)
        {
            var enemyType = subWaveEnemies[UnityEngine.Random.Range(0, subWaveEnemies.Count)];
            subWaveEnemies.Remove(enemyType);
            enemyPools[enemyType].Spawn(spawnAreaManager.GetRandomSpawnPoint(), Quaternion.Euler(0,-90,0));
            enemyCooldown = UnityEngine.Random.Range(SubWave.spawnRate.x, SubWave.spawnRate.y);
        }
        else
        {
            enemyCooldown -= Time.deltaTime;
        }
    }

    private void GetEnemiesListForSubWave()
    {
        subWaveEnemies.Clear();
        var wave = waveSettings.waves[WaveNumberLoop];
        var subWave = wave.subwaves[SubWaveNumber];

        for (int i = 0; i < subWave.enemies.Count; i++)
        {
            var enemyType = subWave.enemies[i].type;
            for (int j = 0; j < subWave.enemies[i].quantity; j++)
            {
                subWaveEnemies.Add(enemyType);

                if (!enemyPools.ContainsKey(enemyType))
                {
                    enemyPools.Add(enemyType, objectPoolsManager.GetPool(enemyType));
                }
            }
        }
    }



    private void StartWave()
    {
        SubWaveNumber = 0;
        StartSubWave();
    }

    private void StartSubWave()
    {
        GetEnemiesListForSubWave();
    }

    private bool IsSubWaveOver()
    {
        return (subWaveEnemies.Count == 0 && NoActiveEnemiesInSubWave());
    }

    private bool IsWaveOver()
    {
        if (loopLastWave) return false;
        return SubWaveNumber >= waveSettings.waves[WaveNumberLoop].subwaves.Count;
    }

    private bool AreAllWavesOver()
    {
        return WaveNumber >= waveSettings.waves.Count;
    }

    private bool NoActiveEnemiesInSubWave()
    {
        foreach (var pool in enemyPools)
        {
            if (pool.Value.ActiveObjectsCount > 0) return false;
        }

        return true;
    }
}
