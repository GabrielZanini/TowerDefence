using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaplayUI : MonoBehaviour
{

    [SerializeField] private CharactersData charactersData;
    [SerializeField] GameObject buttonDefencePrefab;
    [SerializeField] Transform buttonDefenceGroup;

    private Dictionary<DefenceType, ButtonDefence> defenceButtons = new Dictionary<DefenceType, ButtonDefence>();
    [ShowNativeProperty]
    public int DefenceButtonsCount => defenceButtons.Count;

    private void Start()
    {
        if (charactersData == null)
        {
            charactersData = GameManager.Instance.CharactersData;
        }

        CreateButtons();
    }

    [Button]
    private void CreateButtons()
    {
        var defenceTypes = charactersData.DefenceTypes;

        for (int i = 0; i < defenceTypes.Count; i++)
        {
            if (!defenceButtons.ContainsKey(defenceTypes[i]))
            {
                defenceButtons.Add(defenceTypes[i], CreateButton(charactersData.GetDefenceSettings(defenceTypes[i])));
            }

        }
    }

    private ButtonDefence CreateButton(DefenceSettings settings)
    {
        var button = Instantiate(buttonDefencePrefab, buttonDefenceGroup).GetComponent<ButtonDefence>();
        button.SetupButton(settings);
        return button;
    }

    [Button]
    private void Clear()
    {
        defenceButtons.Clear();

        var children = GetComponentsInChildren<ButtonDefence>(true);
        for (int i = 0; i < children.Length; i++)
        {
            if (!Application.isPlaying)
            {
                DestroyImmediate(children[i].gameObject);
            }
            else
            {
                Destroy(children[i].gameObject);
            }
        }
    }


}
