using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterManager : PoolObject
{
    [ShowNativeProperty]
    public int HP { get; protected set; }

    [ShowNativeProperty]
    public int Attack { get; protected set; }

    [ShowNativeProperty] 
    public float Speed { get; protected set; }


    public Action<CharacterManager> OnDeath;


    protected virtual void Awake()
    {
        OnSpawn += Spawned;
        OnDespawn += Despawned;
    }

    protected virtual private void OnDestroy()
    {
        OnSpawn -= Spawned;
        OnDespawn -= Despawned;
    }

    protected virtual void Spawned()
    {

    }

    protected virtual void Despawned()
    {

    }

    [Button]
    protected virtual void Die()
    {
        OnDeath.Invoke(this);
        Despawn();
    }

    public void Despawn()
    {
        pool.Despawn(this);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        
        if (HP <= 0)
        {
            Die();
        }
    }

}
