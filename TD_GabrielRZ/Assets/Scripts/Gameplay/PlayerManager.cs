using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [ShowNativeProperty]
    public int HP { get; protected set; }

    [ShowNativeProperty]
    public int Money { get; protected set; }

    [SerializeField] private PlayerSettings settings;

    public Action OnDeath;

    
    private void Awake()
    {
        SetupPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<EnemyManager>();
            if (enemy != null)
            {
                TakeDamage(enemy.Attack);
                enemy.Despawn();
            }
        }
    }


    private void SetupPlayer()
    {
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
        HP -= damage;

        if (HP <= 0)
        {
            OnDeath.Invoke();
            SetupPlayer();
        }
    }
}
