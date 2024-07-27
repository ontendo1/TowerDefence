using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileSpecSO projectileSpec;
    [HideInInspector] public Vector3 direction;
    public Rigidbody rb;

    void FixedUpdate()
    {
        rb.velocity = direction * projectileSpec.speed;
    }
}
