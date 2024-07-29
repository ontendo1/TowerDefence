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

        //Throw arrow
        GameObject throwedBow = Instantiate(projectilePrfb, projectileSpawnPoint.position, Quaternion.identity);

        //Set heavy ball's direction.
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
