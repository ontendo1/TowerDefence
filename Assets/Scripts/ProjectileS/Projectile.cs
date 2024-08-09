using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolableObject
{
    [SerializeField] protected string objPoolName; //Define object name for pooling.

    [SerializeField] protected ProjectileSpecSO projectileSpec;
    [SerializeField] protected float lifetime = 5;
    [HideInInspector] public Vector3 direction;
    public Rigidbody rb;

    protected ObjectPoolManager objectPooler;

    protected virtual void Start()
    {
        objectPooler = ObjectPoolManager.Instance;
    }

    public virtual void OnSpawn()
    {
        rb.velocity = Vector2.zero;
        StartCoroutine(LifetimeCountdown());
    }

    protected virtual IEnumerator LifetimeCountdown()
    {
        yield return new WaitForSeconds(lifetime);
        objectPooler.AddToPool(objPoolName, gameObject);
    }
}
