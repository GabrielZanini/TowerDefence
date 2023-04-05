using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] [Range(0, 1000)] private int initialInactivesObjects = 10;
    [SerializeField] [Range(0, 1000)] private int maxObjects = 100;
    [ReadOnly] [SerializeField] private List<PoolObject> activeObjects = new List<PoolObject>();
    [ReadOnly] [SerializeField] private List<PoolObject> inactiveObjects = new List<PoolObject>();


    void Start()
    {
        InstantiateInitialObjects();
    }

    public void SetPrefab(GameObject objPrefab)
    {
        prefab = objPrefab;
    }

    [Button]
    private void InstantiateInitialObjects()
    {
        for (int i = 0; i < initialInactivesObjects; i++)
        {
            if (inactiveObjects.Count < initialInactivesObjects)
            {
                InstantiateObject();
            }
        }
    }

    [Button]
    private void Spawn()
    {
        Spawn(transform.position, transform.rotation);
    }


    public void Spawn(Vector3 position, Quaternion rotation)
    {
        PoolObject poolObject;

        // Check if ther is an inactive object in the pool
        if (inactiveObjects.Count == 0)
        {
            InstantiateObject();
        }

        poolObject = inactiveObjects[0];
        inactiveObjects.Remove(poolObject);
        poolObject.gameObject.SetActive(true);
        activeObjects.Add(poolObject);
        poolObject.OnSpawn.Invoke();

        poolObject.transform.position = position;
        poolObject.transform.rotation = rotation;
    }


    [Button]
    public void DespawnFirst()
    {
        if (activeObjects.Count > 0f)
        {
            Despawn(activeObjects[0]);
        }
    }

    public void Despawn(PoolObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
        if (activeObjects.Contains(poolObject))
        {
            activeObjects.Remove(poolObject);
        }
        inactiveObjects.Add(poolObject);
        poolObject.OnDespawn.Invoke();
    }


    [Button]
    private void ClearLists()
    {
        while (activeObjects.Count > 0)
        {
            activeObjects.Remove(activeObjects[0]);
        }

        while (inactiveObjects.Count > 0)
        {
            inactiveObjects.Remove(inactiveObjects[0]);
        }

        var children = GetComponentsInChildren<PoolObject>(true);
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

    private void InstantiateObject()
    {
        // Check if the object limit has been reached
        if (activeObjects.Count + inactiveObjects.Count < maxObjects)
        {
            // Instantiate new Object and add to Inactive list.
            var newObject = Instantiate(prefab, transform);
            PoolObject poolObject = newObject.GetComponent<PoolObject>();
            poolObject.SetPool(this);
            newObject.SetActive(false);
            inactiveObjects.Add(poolObject);
        }
    }
}

public abstract class PoolObject : MonoBehaviour
{
    protected ObjectPool pool;
    public Action OnDespawn;
    public Action OnSpawn;

    public void SetPool(ObjectPool objectPool)
    {
        pool = objectPool;
    }
}