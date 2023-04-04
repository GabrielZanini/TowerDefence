using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceSpawnPreview : MonoBehaviour
{
    [SerializeField] private Material materialGood; 
    [SerializeField] private Material materialBad; 
    [SerializeField] private Renderer renderer;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float smooth = 0.5f;


    [SerializeField] private List<CharacterManager> enemiesTooClose = new List<CharacterManager>();
    [SerializeField] private List<CharacterManager> defencesTooClose = new List<CharacterManager>();

    RaycastHit hit;
    Ray ray;
    bool mouseInPlayableArea = false;

    void Start()
    {
        
    }

    void Update()
    {  
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                transform.position = Vector3.Lerp(transform.position, hit.point, smooth);
                mouseInPlayableArea = true;
            }
            else
            {
                mouseInPlayableArea = hit.collider.tag == "PlayableArea";
            }           
        }

        if (mouseInPlayableArea && enemiesTooClose.Count == 0 && defencesTooClose.Count == 0)
        {
            renderer.material = materialGood;
        }
        else
        {
            renderer.material = materialBad;
        }

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
