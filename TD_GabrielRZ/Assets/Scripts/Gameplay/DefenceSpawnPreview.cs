using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceSpawnPreview : MonoBehaviour
{
    [SerializeField] private Material materialGood;
    [SerializeField] private Material materialBad;
    [SerializeField] private Transform modelParent;
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float smooth = 0.5f;


    private List<CharacterManager> enemiesTooClose = new List<CharacterManager>();
    private List<CharacterManager> defencesTooClose = new List<CharacterManager>();

    private RaycastHit hit;
    private Ray ray;
    private bool mouseInPlayableArea = false;
    private bool canBePlaced = false;
    private GameObject currentModel = null;

    public Action<Vector3> OnConfirm;
    public Action OnCancel;


    private void OnDisable()
    {
        transform.position = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            AddEnemy(other.gameObject.GetComponent<EnemyManager>());
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Defence"))
        {
            AddDefence(other.gameObject.GetComponent<DefenceManager>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            RemoveEnemy(other.gameObject.GetComponent<CharacterManager>());
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Defence"))
        {
            RemoveDefence(other.gameObject.GetComponent<CharacterManager>());
        }
    }

    void Update()
    {
        CheckIfCanPlaceDefence();
        SetRenders(canBePlaced ? materialGood : materialBad);
        ConfirmOrCancel();
    }


    private void ConfirmOrCancel()
    {
        if (Input.GetMouseButtonUp(0) && canBePlaced)
        {
            OnConfirm.Invoke(transform.position);
            gameObject.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            OnCancel.Invoke();
            gameObject.SetActive(false);
        }
    }


    public void ChangeModel(GameObject newModel)
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        currentModel = Instantiate(newModel, modelParent);
        currentModel.transform.rotation = Quaternion.Euler(0, 90, 0);
        renderers = currentModel.GetComponentsInChildren<Renderer>();
        gameObject.SetActive(true);
    }

    private void CheckIfCanPlaceDefence()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (mouseInPlayableArea = Physics.Raycast(ray, out hit, 100.0f, layerMask))
        {
            transform.position = Vector3.Lerp(transform.position, hit.point, smooth);
            mouseInPlayableArea = true;
        }

        canBePlaced = mouseInPlayableArea && enemiesTooClose.Count == 0 && defencesTooClose.Count == 0;
    }

    private void SetRenders(Material material)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = material;
        }
    }

    private void AddEnemy(CharacterManager enemy)
    {
        enemy.OnDeath += RemoveEnemy;
        enemiesTooClose.Add(enemy);
    }

    private void RemoveEnemy(CharacterManager enemy)
    {
        if (enemiesTooClose.Contains(enemy))
        {
            enemy.OnDeath -= RemoveEnemy;
            enemiesTooClose.Remove(enemy);
        }
    }

    private void AddDefence(CharacterManager defence)
    {
        defence.OnDeath += RemoveDefence;
        defencesTooClose.Add(defence);
    }

    private void RemoveDefence(CharacterManager defence)
    {
        if (defencesTooClose.Contains(defence))
        {
            defence.OnDeath -= RemoveDefence;
            defencesTooClose.Remove(defence);
        }
    }


}
