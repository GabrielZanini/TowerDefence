using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
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
        Destroy(gameObject);
    }

    public void TakeDamage()
    {

    }
}
