using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    Soldier,
    Brute,
    Runner,
    Shilder,
    Flyer,
    Healer,
}


public enum DefenceType
{
    Turret,
    Wall,
    Bomb,
    Cannon,
    Catapult,
    MiniTower,
}

public enum GameplayState
{
    MainMenu,
    Playing,
    Pause,
    PlacingDefence,
    GameOver,
    ChangingWave
}


public enum UIState
{
    MainMenu,
    PauseMenu,
    Gameplay
}