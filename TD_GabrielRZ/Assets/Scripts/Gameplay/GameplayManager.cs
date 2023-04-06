using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameplayState gameplayState;
    [SerializeField] private Transform enemyTarget;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerManager playerManager;
    //[SerializeField] private WavesManager wavesManager;
    [SerializeField] private WavesController wavesController;


    public GameplayState GameplayState => gameplayState;
    public Transform EnemyTarget => enemyTarget;
    public PlayerData PlayerData => playerData;
    public PlayerManager PlayerManager => playerManager;
    //public WavesManager WavesManager => wavesManager;
    public WavesController WavesController => wavesController;


    [SerializeField] private DefenceSpawnPreview spawnPreview;


    private DefenceSettings previewSettings = null;
    private ObjectPoolsManager poolsManager;
    private CharactersData charactersData;

    private Dictionary<DefenceType, ObjectPool> defencePools = new Dictionary<DefenceType, ObjectPool>();


    [HideInInspector] public UnityEvent OnMainMenu;
    [HideInInspector] public UnityEvent OnGameStart;
    [HideInInspector] public UnityEvent OnGameOver;
    [HideInInspector] public UnityEvent OnPause;
    [HideInInspector] public UnityEvent OnContinue;
    [HideInInspector] public UnityEvent OnChangeGameplayState;
    


    private void Awake()
    {
        spawnPreview.OnConfirm += OnPreviewConfirm;
        spawnPreview.OnCancel += OnPreviewCancel;

        playerManager.OnDeath.AddListener(GameOver);
    }

    private void OnDestroy()
    {
        spawnPreview.OnConfirm -= OnPreviewConfirm;
        spawnPreview.OnCancel -= OnPreviewCancel;

        playerManager.OnDeath.RemoveListener(GameOver);
    }

    private void Start()
    {
        charactersData = GameManager.Instance.CharactersData;
        poolsManager = GameManager.Instance.ObjectPoolsManager;
        MainMenu();
    }



    public void SetPlacingDefence(DefenceType type)
    {
        ChangGamplayState(GameplayState.PlacingDefence);
        previewSettings = charactersData.GetDefenceSettings(type);
        spawnPreview.ChangeModel(previewSettings.originalModel);
    }

    private void OnPreviewConfirm(Vector3 position)
    {
        ChangGamplayState(GameplayState.Playing);
        if (playerManager.Money > previewSettings.levels[0].price)
        {
            var defenceType = previewSettings.type;

            if (!defencePools.ContainsKey(defenceType))
            {
                defencePools.Add(defenceType, poolsManager.GetPool(defenceType));
            }

            var pool = defencePools[defenceType];
            pool.Spawn(position, Quaternion.Euler(0, 90, 0));

            playerManager.AddMoney(-previewSettings.levels[0].price);
        }        
    }

    private void OnPreviewCancel()
    {
        ChangGamplayState(GameplayState.Playing);
    }


    public void ClearDefences()
    {
        foreach (var defencePool in defencePools)
        {
            defencePool.Value.DespawnAll();
        }
    }


    [Button]
    public void MainMenu()
    {
        wavesController.ResetWaves();
        OnMainMenu.Invoke();
        ChangGamplayState(GameplayState.MainMenu);
    }

    [Button]
    public void GameStart()
    {
        Time.timeScale = 1;
        playerManager.SetupPlayer();
        playerData.ResetScore();
        ChangGamplayState(GameplayState.Playing);
        OnGameStart.Invoke();
    }

    [Button]
    public void GameOver()
    {
        playerData.SaveScoreLog();
        ChangGamplayState(GameplayState.GameOver);
        ClearDefences();
        wavesController.ResetWaves();
        OnPause.Invoke();
    }

    [Button]
    public void Pause()
    {
        Time.timeScale = 0;
        ChangGamplayState(GameplayState.Pause);
        OnPause.Invoke();
    }

    [Button]
    public void Continue()
    {
        Time.timeScale = 1;
        ChangGamplayState(GameplayState.Playing);
        OnContinue.Invoke();
    }

    private void ChangGamplayState(GameplayState newState)
    {
        gameplayState = newState;
        OnChangeGameplayState.Invoke();
    }
}
