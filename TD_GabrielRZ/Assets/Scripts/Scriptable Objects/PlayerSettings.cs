using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Settings", menuName = "Scriptable Objects/Player Settings", order = 1)]
public class PlayerSettings : ScriptableObject
{
    public int hp = 100;
    public int initialMoney = 200;
}
