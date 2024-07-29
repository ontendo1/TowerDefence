using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileSpecSO projectileSpec;
    [SerializeField] private float lifetime = 5;
    [HideInInspector] public Vector3 direction;
    public Rigidbody rb;

    void Start()
    {
        StartCoroutine(LifetimeCountdown());
    }

    void FixedUpdate()
    {
        rb.velocity = direction * projectileSpec.speed;
    }

    IEnumerator LifetimeCountdown()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
