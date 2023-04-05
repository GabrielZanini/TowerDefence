using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameplayState gameplayState;
    public GameplayState GameplayState { get { return gameplayState; } }

    [SerializeField] private Transform enemyTarget;
    public Transform EnemyTarget { get { return enemyTarget; } }

    [SerializeField] private PlayerData playerData;
    public PlayerData PlayerData { get { return playerData; } }

    [SerializeField] private EnemySpawner enemySpawner;
    public EnemySpawner EnemySpawner { get { return enemySpawner; } }

    [SerializeField] private DefenceSpawnPreview spawnPreview;
    [SerializeField] private PlayerManager playerManager;


    private DefenceSettings previewSettings = null;
    private ObjectPoolsManager poolsManager;
    private CharactersData charactersData;


    private void Awake()
    {
        spawnPreview.OnConfirm += OnPreviewConfirm;
        spawnPreview.OnCancel += OnPreviewCancel;
    }

    private void OnDestroy()
    {
        spawnPreview.OnConfirm -= OnPreviewConfirm;
        spawnPreview.OnCancel -= OnPreviewCancel;
    }

    private void Start()
    {
        charactersData = GameManager.Instance.CharactersData;
        poolsManager = GameManager.Instance.ObjectPoolsManager;
    }



    public void SetPlacingDefence(DefenceType type)
    {
        gameplayState = GameplayState.PlacingDefence;
        previewSettings = charactersData.GetDefenceSettings(type);
        spawnPreview.ChangeModel(previewSettings.originalModel);
    }

    private void OnPreviewConfirm(Vector3 position)
    {
        gameplayState = GameplayState.Playing;
        poolsManager.GetPool(previewSettings.type).Spawn(position, Quaternion.Euler(0,90,0));
        playerData.AddMoney(-previewSettings.levels[0].price);
    }

    private void OnPreviewCancel()
    {
        gameplayState = GameplayState.Playing;
    }

}
