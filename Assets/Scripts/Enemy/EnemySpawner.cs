using System.Collections;
using System.Collections.Generic;
using PathCreation;
using PathCreation.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float enemySpawnTime;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private PathCreator path;
    [SerializeField] private float enemySpeed = 1.1f;

    [Space(6)]
    [Header("Health")]
    [SerializeField] private float defaultHealthValue;
    [SerializeField] private float healthValueIncreaseSens;

    void Start()
    {
        StartCoroutine(SpawnCycle());
    }

    IEnumerator SpawnCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemySpawnTime);

            GameObject enemyToSpawn = ObjectPoolManager.Instance.SpawnFromPool("Enemy", Vector3.zero, Quaternion.identity, enemyPrefab);
        
            //Reset path follower for take enemy to starting position.
            PathFollower pathFollower = enemyToSpawn.GetOrAddComponent<PathFollower>();
            pathFollower.pathCreator = path;
            pathFollower.speed = enemySpeed;

            if (enemyToSpawn.TryGetComponent(out EnemyHealth enemyHealth))
            {
                enemyHealth.Health = defaultHealthValue;
                enemyHealth.defaultHealth = defaultHealthValue;
            }

            //Increases health value for get harder game after every monster spawn. 
            defaultHealthValue += healthValueIncreaseSens;
        }
    }
}
