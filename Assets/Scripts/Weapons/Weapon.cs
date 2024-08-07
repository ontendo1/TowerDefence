using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float lifetime = 10;
    public float shotCycle = 4;

    [SerializeField] protected GameObject projectilePrfb;
    [SerializeField] protected Transform projectileSpawnPoint;
    [SerializeField] protected Animator animator;


    protected Vector3 direction;
    protected Transform targetEnemy;

    protected float angleInDegrees
    ;
    protected HashSet<Transform> enemiesInRange = new();

    protected int ANIM_SHOT_TRIGGER;

    protected virtual void Start()
    {
        ANIM_SHOT_TRIGGER = Animator.StringToHash("Shot");

        StartCoroutine(LifetimeTimer());
        StartCoroutine(ShootingCycle());
    }

    protected void FixedUpdate()
    {

    }

    public virtual void ShotProjectile()
    {

    }

    IEnumerator ShootingCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotCycle);

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

        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.transform);
            print("Added");
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);
            print("Removed");
        }
    }
}


