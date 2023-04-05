using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceGun : MonoBehaviour
{
    [SerializeField] private DefenceManager defenceManager;
    [SerializeField] private Transform gun;
    [SerializeField] private Transform muzzle;
    [SerializeField] private bool followEnemy;
    [SerializeField] private bool areaDamage;

    private List<CharacterManager> enemiesInRange = new List<CharacterManager>();

    private CharacterManager enemyTarget = null;
    private float cooldown = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            AddEnemy(other.gameObject.GetComponent<EnemyManager>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            RemoveEnemy(other.gameObject.GetComponent<CharacterManager>());
        }
    }

    private void Update()
    {
        GetTarget();
        if (followEnemy) PointGunToEnemy();
        ShootTarget();
    }


    private void GetTarget()
    {
        if (enemyTarget == null)
        {
            if (enemiesInRange.Count > 0)
            {
                enemyTarget = enemiesInRange[0];
                enemyTarget.OnDeath += ReleaseTarget;
            }
        }
    }

    private void ReleaseTarget(CharacterManager characterManager)
    {
        characterManager.OnDeath -= ReleaseTarget;

        enemyTarget = null;
    }

    private void PointGunToEnemy()
    {
        if (enemyTarget != null)
        {
            gun.LookAt(enemyTarget.transform.position + Vector3.up);
        }
        else
        {
            gun.rotation = Quaternion.identity;
        }
    }

    private void ShootTarget()
    {
        if (cooldown <= 0f)
        {
            if (areaDamage)
            {
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    enemiesInRange[i].TakeDamage(defenceManager.Attack);
                }
            }
            else
            {
                enemyTarget.TakeDamage(defenceManager.Attack);
            }

            cooldown = defenceManager.Speed;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    private void AddEnemy(CharacterManager enemy)
    {
        enemy.OnDeath += RemoveEnemy;
        enemiesInRange.Add(enemy);
    }

    private void RemoveEnemy(CharacterManager enemy)
    {
        if (enemiesInRange.Contains(enemy))
        {
            enemy.OnDeath -= RemoveEnemy;
            enemiesInRange.Remove(enemy);
        }

        if (enemy == enemyTarget)
        {
            ReleaseTarget(enemy);
        }
    }

}
