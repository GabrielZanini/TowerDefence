using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private CharactersData charactersData;
    public CharactersData CharactersData { get { return charactersData; } }

    [SerializeField] private ObjectPoolsManager objectPoolsManager;
    public ObjectPoolsManager ObjectPoolsManager { get { return objectPoolsManager; } }

    [SerializeField] private GameplayManager gameplayManager;
    public GameplayManager GameplayManager { get { return gameplayManager; } }



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }



}
