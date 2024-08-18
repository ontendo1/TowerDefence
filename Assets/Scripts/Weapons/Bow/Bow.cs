using UnityEngine;

public class Bow : Weapon
{
    public override void ShotProjectile()
    {
        base.ShotProjectile();

        //Throw an arrow
        GameObject throwedBow = objectPooler.SpawnFromPool("Arrow", projectileSpawnPoint.position, Quaternion.Euler(Vector3.up * angleInDegrees), projectilePrfb);

        //Set arrow's direction.
        if (throwedBow.TryGetComponent(out Projectile projectile))
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
