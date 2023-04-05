using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [ShowNativeProperty]
    public int HP { get; protected set; }

    [ShowNativeProperty]
    public int Money { get; protected set; }

    [SerializeField] private PlayerSettings settings;

    public UnityEvent OnDeath;

    
    private void Awake()
    {
        SetupPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var enemy = other.gameObject.GetComponentInParent<EnemyManager>();
            if (enemy != null)
            {
                TakeDamage(enemy.Attack);
                enemy.Despawn();
            }
        }
    }


    private void SetupPlayer()
    {
        Debug.Log("PlayerManager :: SetupPlayer");
        HP = settings.hp;
        Money = settings.initialMoney;
    }

    public void healHp()
    {
        HP = settings.hp;
    }

    public void AddMoney(int value)
    {
        Money += value;

        if (Money <= 0)
        {
            Money = 0;
        }
    }


    private void TakeDamage(int damage)
    {
        HP = HP - damage;

        if (HP <= 0)
        {
            OnDeath.Invoke();
            SetupPlayer();
        }
    }
}
