using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour, IPoolableObject
{
    public float lifetime = 10;
    public float shotCycle = 4;

    [Space(4)]
    [SerializeField] protected string weaponName;
    [SerializeField] protected GameObject projectilePrfb;
    [SerializeField] protected Transform projectileSpawnPoint;
    [SerializeField] protected Animator animator;

    protected Vector3 direction;
    protected Transform targetEnemy;

    protected float angleInDegrees;

    protected List<Transform> enemiesInRange = new();

    protected int ANIM_SHOT_TRIGGER;

    protected ObjectPoolManager objectPooler;

    [HideInInspector] public WeaponPlacementValidator placementValidator;

    public virtual void OnSpawn()
    {
        ANIM_SHOT_TRIGGER = Animator.StringToHash("Shot");

        StartCoroutine(LifetimeTimer());
        StartCoroutine(ShootingCycle());

        objectPooler = ObjectPoolManager.Instance;

        enemiesInRange.Clear();
        animator.ResetTrigger(ANIM_SHOT_TRIGGER);
    }

    public virtual void ShotProjectile()
    {

    }

    IEnumerator ShootingCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotCycle);

            //Check the enemies in range is active or inactive
            CheckActiveEnemiesInRange();

            if (enemiesInRange.Count < 1) continue;

            //Get target enemy; is first enemy in range.
            targetEnemy = enemiesInRange.First();

            //Calculate target enemy's position:
            float targetPosX = targetEnemy.position.x - transform.position.x;
            float targetPosZ = targetEnemy.position.z - transform.position.z;

            direction = new Vector3(targetPosX, 0, targetPosZ).normalized;

            angleInDegrees = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            //Turn to enemy
            transform.rotation = Quaternion.Euler(Vector3.up * angleInDegrees);

            animator.SetTrigger(ANIM_SHOT_TRIGGER);
        }
    }

    IEnumerator LifetimeTimer()
    {
        yield return new WaitForSeconds(lifetime);

        Destroy(placementValidator); //Removes placement validator component from the placed grid.
        objectPooler.AddToPool(weaponName, gameObject);
    }

    void CheckActiveEnemiesInRange()
    {
        HashSet<Transform> inactiveEnemies = new();

        foreach (Transform enemy in enemiesInRange)
        {
            if (!enemy.gameObject.activeSelf)
            {
                inactiveEnemies.Add(enemy);
            }
        }

        foreach (Transform inactiveEnemy in inactiveEnemies)
        {
            enemiesInRange.Remove(inactiveEnemy);
        }

    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.transform);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);
        }
    }
}


