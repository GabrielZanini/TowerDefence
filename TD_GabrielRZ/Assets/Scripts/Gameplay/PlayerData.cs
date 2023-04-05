using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private new string name = "";
    public string Name { get { return name; } }

    [SerializeField] private int score;
    public int Score { get { return score; } }


    public void AddScore(int points)
    {
        score += points;
        if (score < 0)
        {
            score = 0;
        }
    }
}
