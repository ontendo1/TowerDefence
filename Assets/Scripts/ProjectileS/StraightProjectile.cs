using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : Projectile
{
    void FixedUpdate()
    {
        rb.velocity = direction * projectileSpec.speed;
    }
}
