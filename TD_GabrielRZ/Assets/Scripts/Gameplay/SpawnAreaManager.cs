using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnAreas;

    public Vector3 GetRandomSpawnPoint()
    {
        var spawnArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
        var offset = new Vector3(spawnArea.localScale.x * Random.Range(-0.5f, 0.5f), 0f, spawnArea.localScale.z * Random.Range(-0.5f, 0.5f));
        return spawnArea.position + offset;
    }
}
