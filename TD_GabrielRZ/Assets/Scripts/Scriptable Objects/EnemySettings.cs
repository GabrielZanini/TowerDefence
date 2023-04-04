using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Settings", menuName = "ScriptableObjects", order = 1)]
public class EnemySettings : ScriptableObject
{
    public string name ;
    public float hp = 100;
    public float speed = 3f;
    public float eliteBonus = 2f;
    public float eliteChance = 0.01f;
}
