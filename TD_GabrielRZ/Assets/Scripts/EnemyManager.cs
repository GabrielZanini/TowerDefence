using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Action<EnemyManager> OnDeath;

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
        Destroy(gameObject);
    }

}
