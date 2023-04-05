using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Settings", menuName = "Scriptable Objects/Enemy Settings", order = 1)]
public class EnemySettings : ScriptableObject
{
    public EnemyType type = EnemyType.Soldier;
    public string name = "Enemy";
    public GameObject originalModel;
    public GameObject prefab;

    public int hp = 100;
    public float speed = 3f;
    public int attack = 10;
    public int moneyDrop = 5;
    public int pointsWorth = 10;
    public float eliteBonus = 2f;
    public float eliteChance = 0.01f;
}
