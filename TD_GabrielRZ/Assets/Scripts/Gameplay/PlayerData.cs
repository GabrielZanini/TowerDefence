using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] string playerName = "PLAYER NAME PLAYER NAME";

    [ShowNativeProperty]
    public string PlayerName { get { return playerName; } }

    [SerializeField] private int score = 0;
    public int Score { get { return score; } }


    [SerializeField] private List<ScoreLog> scoreHistory = new List<ScoreLog>();


    private void Awake()
    {
        LoadScoreFile();
    }

    private void OnDestroy()
    {
        SaveScoreFile();
    }

    public void AddScore(int points)
    {
        score += points;
        if (score < 0)
        {
            score = 0;
        }
    }

    public void SetPlayerName(string newName)
    {
        playerName = newName;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void SaveScoreLog()
    {
        scoreHistory.Add(new ScoreLog(PlayerName, Score));
    }

    private void SaveScoreFile()
    {

    }

    private void LoadScoreFile()
    {

    }

}




[Serializable]
public class ScoreLog
{
    public string playerName;
    public int score;

    public ScoreLog(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}
