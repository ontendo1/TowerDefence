using UnityEngine;

public class Bow : Weapon
{
    protected override void Start()
    {
        base.Start();
    }

    public override void ShotProjectile()
    {
        base.ShotProjectile();

        GameObject throwedBow = Instantiate(projectilePrfb, projectileSpawnPoint.position, Quaternion.identity);

        if (throwedBow.TryGetComponent(out Projectile projectile))
        {
            projectile.direction = Vector3.forward;
        }
    }
}
