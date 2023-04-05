using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private EnemyType type = EnemyType.Soldier;


    private NavMeshAgent navMeshAgent;
 
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameManager.Instance.GameplayManager.EnemyTarget;

    }

    private void Update()
    {
        navMeshAgent.destination = target.position;
    }
}
