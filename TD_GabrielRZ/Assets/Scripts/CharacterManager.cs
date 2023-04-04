using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterManager : PoolObject
{
    public Action<CharacterManager> OnDeath;


    void Start()
    {

    }

    void Update()
    {

    }

    [Button]
    void Die()
    {
        OnDeath.Invoke(this);
        pool.Despawn(this);
        Destroy(gameObject);
    }

    public void TakeDamage()
    {

    }
}
