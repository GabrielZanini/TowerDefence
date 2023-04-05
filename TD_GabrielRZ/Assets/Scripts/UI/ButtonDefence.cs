using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDefence : MonoBehaviour
{
    [SerializeField] private DefenceType type;
    [SerializeField] private Text name;
    [SerializeField] private Text price;

    private Button button;
    private int priceValue;
    private GameplayManager gameplayManager;

    void Start()
    {
        gameplayManager = GameManager.Instance.GameplayManager;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void Update()
    {
        button.interactable = gameplayManager.PlayerManager.Money > priceValue;
    }

    public void SetupButton(DefenceSettings settings)
    {
        type = settings.type;
        name.text = settings.name;
        price.text = settings.levels[0].price.ToString();
        priceValue = settings.levels[0].price;
    }


    private void OnClick()
    {
        gameplayManager.SetPlacingDefence(type);
    }
}
