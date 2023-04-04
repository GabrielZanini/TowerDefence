using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Defence Settings", menuName = "Scriptable Objects/Defence Settings", order = 1)]
public class DefenceSettings : MonoBehaviour
{
    public DefenceType type;
    public string name = "Defence";
    public GameObject originalModel;
    

    public List<DefenceLevel> levels;

    private void OnValidate()
    {
        for (int i=0; i< levels.Count; i++)
        {
            levels[i].SetType(type);
        }
    }
}

public class DefenceLevel
{
    private DefenceType type;
    public float hp = 100;
    public float speed = 3f;

    public void SetType(DefenceType defenceType)
    {
        type = defenceType;
    }
}