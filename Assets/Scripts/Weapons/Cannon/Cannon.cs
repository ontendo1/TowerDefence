using UnityEngine;

public class Cannon : Weapon
{
    public override void ShotProjectile()
    {
        base.ShotProjectile();

        //Throw a cannon
        GameObject throwedCannon = objectPooler.SpawnFromPool("Cannon", projectileSpawnPoint.position, Quaternion.identity, projectilePrfb);

        //Set heavy ball's direction.
        if (throwedCannon.TryGetComponent(out Projectile projectile))
        {
            projectile.direction = direction;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
