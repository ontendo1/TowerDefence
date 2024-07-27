using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float lifetime = 10;
    public float shotCycle = 4;

    [SerializeField] protected GameObject projectilePrfb;
    [SerializeField] protected Transform projectileSpawnPoint;
    [SerializeField] protected Animator animator;

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
        print("fired a shot");
    }

    IEnumerator ShootingCycle()
    {
        while (true)
        {

            yield return new WaitForSeconds(shotCycle);

            animator.SetTrigger(ANIM_SHOT_TRIGGER);
        }
    }

    IEnumerator LifetimeTimer()
    {
        yield return new WaitForSeconds(lifetime);

        gameObject.SetActive(false);
    }
}
