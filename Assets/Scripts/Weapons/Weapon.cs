using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float lifetime = 10;
    public float shotCycle = 4;

    [SerializeField] protected GameObject projectilePrfb;
    [SerializeField] protected Transform projectileSpawnPoint;
    [SerializeField] protected Animator animator;

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


