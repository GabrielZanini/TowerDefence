using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    public enum TypesOfEnemy
    {
        Soldier,
        Brute,
        Runner,
        Shilder,
        Flyer,
        Healer,
    }

    public enum EnemyRank
    {
        Normal,
        Elite
    }

    public enum TypeOfDefence
    {
        Turret,
        Wall,
        Bomb,
        Cannon,
        Catapult,
        MiniTower,
    }

    public enum TypeOfObjective
    {
        GoTo,
        Destroy,
        Avoid
    }
}
