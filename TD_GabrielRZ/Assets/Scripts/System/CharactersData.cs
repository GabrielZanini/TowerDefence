using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersData : MonoBehaviour
{

    [SerializeField] private List<DefenceTypeSettings> defences;
    [SerializeField] private List<EnemyTypeSettings> enemies;

    private  List<DefenceType> defenceTypes = new List<DefenceType>();
    public List<DefenceType> DefenceTypes { get { return defenceTypes; } }

    private  List<EnemyType> enemyTypes = new List<EnemyType>();
    public List<EnemyType> EnemyTypes { get { return enemyTypes; } }

    private void OnValidate()
    {
        defenceTypes.Clear();
        enemyTypes.Clear();

        for (int i = 0; i < defences.Count; i++)
        {
            defenceTypes.Add(defences[i].type);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemyTypes.Add(enemies[i].type);
        }
    }

    public DefenceSettings GetDefenceSettings(DefenceType type)
    {
        for (int i = 0; i < defences.Count; i++)
        {
            if (defences[i].type == type)
            {
                return defences[i].settings;
            }
        }

        return null;
    }

    public EnemySettings GetEnemySettings(EnemyType type)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].type == type)
            {
                return enemies[i].settings;
            }
        }

        return null;
    }

    

    [Serializable]
    private class DefenceTypeSettings
    {
        public DefenceType type;
        public DefenceSettings settings;
    }

    [Serializable]
    private class EnemyTypeSettings
    {
        public EnemyType type;
        public EnemySettings settings;
    }
}
