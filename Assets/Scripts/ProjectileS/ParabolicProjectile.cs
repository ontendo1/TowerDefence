using UnityEngine;

public class ParabolicProjectile : Projectile
{
    void FixedUpdate()
    {
        rb.velocity = direction * projectileSpec.speed;
    }
}
