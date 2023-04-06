using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private WavesController wavesController;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameplayMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
        
    [SerializeField] private InputField playerNameField;
    [SerializeField] private Button startGameButton;

    [SerializeField] private Text playerName;
    [SerializeField] private Text playerHealthLabel;
    [SerializeField] private Text moneyLabel;
    [SerializeField] private Text scoreLabel;
    [SerializeField] private Text waveLabel;

    [SerializeField] private Text playerNameGameover;
    [SerializeField] private Text scoreLabelGameover;


    void Awake()
    {
        gameplayManager.OnChangeGameplayState.AddListener(SetMenuUI);
    }

    private void OnDestroy()
    {
        gameplayManager.OnChangeGameplayState.RemoveListener(SetMenuUI);
    }

    void Update()
    {
        if (gameplayManager.GameplayState == GameplayState.Playing || gameplayManager.GameplayState == GameplayState.PlacingDefence)
        {
            playerName.text = gameplayManager.PlayerData.PlayerName;
            playerHealthLabel.text = gameplayManager.PlayerManager.HP.ToString();
            moneyLabel.text = gameplayManager.PlayerManager.Money.ToString();
            scoreLabel.text = gameplayManager.PlayerData.Score.ToString();
            waveLabel.text = wavesController.WaveNumber.ToString();
        }
        else if (gameplayManager.GameplayState == GameplayState.MainMenu)
        {
            startGameButton.interactable = (playerNameField.text != "");          
        }
    }

    private void SetMenuUI()
    {
        mainMenu.SetActive(false);
        gameplayMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

        if (gameplayManager.GameplayState == GameplayState.MainMenu)
        {
            mainMenu.SetActive(true);
            
        }
        else if (gameplayManager.GameplayState == GameplayState.Playing)
        {
            gameplayMenu.SetActive(true);
        }
        else if (gameplayManager.GameplayState == GameplayState.Pause)
        {
            pauseMenu.SetActive(true);
        }
        else if (gameplayManager.GameplayState == GameplayState.GameOver)
        {
            gameOverMenu.SetActive(true);
        }
    }
}
