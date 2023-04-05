using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPoolsManager : MonoBehaviour
{
    [SerializeField] private CharactersData charactersData;

    private Dictionary<DefenceType, ObjectPool> defencePools = new Dictionary<DefenceType, ObjectPool>();
    private Dictionary<EnemyType, ObjectPool> enemyPools = new Dictionary<EnemyType, ObjectPool>();

    [ShowNativeProperty]
    public int DefencePoolsCount => defencePools.Count;
    [ShowNativeProperty]
    public int EnemyPoolsCount => enemyPools.Count;

    private void Awake()
    {
        if (charactersData == null)
        {
            charactersData = GameManager.Instance.CharactersData;
        }
    }

    private void Start()
    {
        CreateObjectPools();
    }

    [Button]
    private void CreateObjectPools()
    {
        var defenceTypes = charactersData.DefenceTypes;
        var enemyTypes = charactersData.EnemyTypes;

        for (int i = 0; i < defenceTypes.Count; i++)
        {
            if (!defencePools.ContainsKey(defenceTypes[i]))
            {
                defencePools.Add(defenceTypes[i], CreatePool(charactersData.GetDefenceSettings(defenceTypes[i]).prefab));
            }
            
        }

        for (int i = 0; i < enemyTypes.Count; i++)
        {
            if (!enemyPools.ContainsKey(enemyTypes[i]))
            {
                enemyPools.Add(enemyTypes[i], CreatePool(charactersData.GetEnemySettings(enemyTypes[i]).prefab));
            }
        }
    }

    private ObjectPool CreatePool(GameObject prefab)
    {
        GameObject gameObject = new GameObject(prefab.name + " Pool");
        gameObject.transform.parent = transform;
        ObjectPool pool = (ObjectPool)gameObject.AddComponent(typeof(ObjectPool));
        pool.SetPrefab(prefab);
        return pool;
    }

    [Button]
    private void Clear()
    {
        defencePools.Clear();
        enemyPools.Clear();

        var children = GetComponentsInChildren<ObjectPool>(true);
        for (int i = 0; i < children.Length; i++)
        {
            if (!Application.isPlaying)
            {
                DestroyImmediate(children[i].gameObject);
            }
            else
            {
                Destroy(children[i].gameObject);
            }
        }
    }


    public ObjectPool GetPool(DefenceType defenceType)
    {
        return defencePools[defenceType];
    }

    public ObjectPool GetPool(EnemyType enemyType)
    {
        return enemyPools[enemyType];
    }

}
