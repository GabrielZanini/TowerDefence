using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenceManager : CharacterManager
{
    [SerializeField] private DefenceType type;
    public DefenceType Type { get { return type; } }

    [ShowNativeProperty]
    public int Level { get; private set; }


    [SerializeField] private GameObject rangeTrigger;
    [SerializeField] private Button UpgradeButton;
    private DefenceSettings settings;


    protected override void Awake()
    {
        base.Awake();
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



  
    //private void OnMouseEnter()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
            
    //    }

    //    UpgradeButton.gameObject.SetActive(true);
    //}


    //private void OnMouseExit()
    //{
    //    StartCoroutine(ShowUpgradeUI(2f));
    //}

    //IEnumerator ShowUpgradeUI(float duration)
    //{

    //    yield return new WaitForSeconds(duration);

    //    UpgradeButton.gameObject.SetActive(false);
    //}
}
