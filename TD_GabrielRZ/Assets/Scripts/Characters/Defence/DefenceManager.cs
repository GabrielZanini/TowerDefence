using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceManager : CharacterManager
{
    [SerializeField] private DefenceType type;
    public DefenceType Type { get { return type; } }

    [ShowNativeProperty]
    public int Level { get; private set; }


    [SerializeField] private GameObject rangeTrigger;
    private DefenceSettings settings;


    private void Awake()
    {
        settings = GameManager.Instance.CharactersData.GetDefenceSettings(Type);
        rangeTrigger.transform.localScale = Vector3.one * settings.triggerSize;
    }


    protected override void Spawned()
    {
        base.Spawned();
        SetLevel(0);
    }

    protected override void Despawned()
    {
        base.Despawned();
        SetLevel(0);
    }

    private void SetLevel(int level)
    {
        Level = level;

        HP = settings.levels[Level].hp;
        Speed = settings.levels[Level].speed;
        Attack = settings.levels[Level].attack;
    }

    public void LevelUp()
    {
        if (Level + 1 < settings.levels.Count)
        {
            SetLevel(Level + 1);
        }
    }

}
