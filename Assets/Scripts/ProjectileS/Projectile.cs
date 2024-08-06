using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected ProjectileSpecSO projectileSpec;
    [SerializeField] protected float lifetime = 5;
    [HideInInspector] public Vector3 direction;
    public Rigidbody rb;

    protected virtual void Start()
    {
        StartCoroutine(LifetimeCountdown());
    }

    protected virtual IEnumerator LifetimeCountdown()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
