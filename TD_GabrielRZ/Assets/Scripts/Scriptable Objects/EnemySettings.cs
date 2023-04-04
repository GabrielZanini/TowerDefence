using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Settings", menuName = "Scriptable Objects/Enemy Settings", order = 1)]
public class EnemySettings : ScriptableObject
{
    public string name = "Enemy";
    public GameObject originalModel;
    public float hp = 100;
    public float speed = 3f;
    public float eliteBonus = 2f;
    public float eliteChance = 0.01f;
}
