using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    [ShowNativeProperty]
    public EnemyType Type { get; private set; }

    [ShowNativeProperty]
    public bool IsElite { get; protected set; }

    [ShowNativeProperty]
    public int MoneyDrop { get; protected set; }

    [ShowNativeProperty]
    public int PointsWorth { get; protected set; }


    private EnemySettings settings;
    private PlayerData playerData;
    private PlayerManager playerManager;


    protected override void Awake()
    {
        base.Awake();
        settings = GameManager.Instance.CharactersData.GetEnemySettings(Type);
        playerData = GameManager.Instance.GameplayManager.PlayerData;
        playerManager = GameManager.Instance.GameplayManager.PlayerManager;
    }

    protected override void Die()
    {
        playerData.AddScore(PointsWorth);
        playerManager.AddMoney(MoneyDrop);

        base.Die();
    }

    protected override void Spawned()
    {
        base.Spawned();
        SetStatus();
    }

    private void SetStatus()
    {
        IsElite = UnityEngine.Random.Range(0f, 1f) < settings.eliteChance;

        if (IsElite)
        {
            HP = Mathf.RoundToInt(settings.hp * settings.eliteBonus);
            Speed = settings.speed * settings.eliteBonus;
            Attack = Mathf.RoundToInt(settings.attack * settings.eliteBonus);

            MoneyDrop = Mathf.RoundToInt(settings.moneyDrop * settings.eliteBonus);
            PointsWorth = Mathf.RoundToInt(settings.pointsWorth * settings.eliteBonus);
            transform.localScale = Vector3.one * settings.eliteBonus;
        }
        else
        {
            HP = settings.hp;
            Speed = settings.speed;
            Attack = settings.attack;

            MoneyDrop = settings.moneyDrop;
            PointsWorth = settings.pointsWorth;
            transform.localScale = Vector3.one;
        }


    }
}
