using UnityEngine;

public class Cannon : Weapon
{
    protected override void Start()
    {
        base.Start();
    }

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

        //Calls methods for reset object status in previous use. Makes the object brand new.
        if (throwedCannon.TryGetComponent(out IPoolableObject poolableObject))
        {
            poolableObject.OnSpawn();
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
