using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceDefence : MonoBehaviour
{
    [SerializeField] private Material materialGood; 
    [SerializeField] private Material materialBad; 
    [SerializeField] private Renderer renderer;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float smooth = 0.5f;


    [SerializeField] private List<EnemyManager> enemiesTooClose = new List<EnemyManager>();
    [SerializeField] private List<DefenceManager> defencesTooClose = new List<DefenceManager>();

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
            RemoveEnemy(other.gameObject.GetComponent<EnemyManager>());
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Defence"))
        {
            RemoveDefence(other.gameObject.GetComponent<DefenceManager>());
        }
    }

    private void AddEnemy(EnemyManager enemy)
    {
        enemy.OnDeath += RemoveEnemy;
        enemiesTooClose.Add(enemy);
    }

    private void RemoveEnemy(EnemyManager enemy)
    {        
        if (enemiesTooClose.Contains(enemy))
        {
            enemy.OnDeath -= RemoveEnemy;
            enemiesTooClose.Remove(enemy);
        }
    }

    private void AddDefence(DefenceManager defence)
    {
        defence.OnDeath += RemoveDefence;
        defencesTooClose.Add(defence);
    }

    private void RemoveDefence(DefenceManager defence)
    {
        if (defencesTooClose.Contains(defence))
        {
            defence.OnDeath -= RemoveDefence;
            defencesTooClose.Remove(defence);
        }
    }


}
