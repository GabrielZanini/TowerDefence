using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

[CreateAssetMenu(fileName = "Defence Settings", menuName = "Scriptable Objects/Defence Settings", order = 1)]
public class DefenceSettings : ScriptableObject
{
    public DefenceType type = DefenceType.Turret;
    public string name = "Defence";
    public GameObject originalModel;
    public GameObject prefab;
    public int triggerSize = 10;

    public List<DefenceLevel> levels = new List<DefenceLevel>(1);

}

[Serializable]
public class DefenceLevel
{
    public int price = 0;
    public int hp = 100;
    public float speed = 3f;
    public int attack = 10;
}