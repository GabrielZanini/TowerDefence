using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Settings", menuName = "Scriptable Objects/Wave Settings", order = 1)]
public class WaveSettings : ScriptableObject
{
    public float dificultyScalePerWave = 1f;
    public bool onlyScaleDificultyAfterLast = false;
    public List<Wave> waves = new List<Wave>();
}

[Serializable]
public class Wave
{
    public int prize;
    public List<SubWave> subwaves = new List<SubWave>();
}


[Serializable]
public class SubWave
{
    [MinMaxSlider(0.1f, 10.0f)]
    public Vector2 spawnRate = new Vector2(0.5f,5f);
    public List<WaveEnemy> enemies = new List<WaveEnemy>();
}

[Serializable]
public class WaveEnemy
{
    [Range(1, 1000)]
    public int quantity = 10;
    public EnemyType type = EnemyType.Soldier;
}
