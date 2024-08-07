using UnityEngine;

public class Catapult : Weapon
{
    protected override void Start()
    {
        base.Start();
    }

    public override void ShotProjectile()
    {
        base.ShotProjectile();

        GameObject throwedBow = Instantiate(projectilePrfb, projectileSpawnPoint.position, Quaternion.Euler(Vector3.up * angleInDegrees));

        //Set direction and vertical force of catapult's heavy ball.
        if (throwedBow.TryGetComponent(out ParabolicProjectile projectile))
        {
            projectile.direction = direction;
            projectile.targetTransform = targetEnemy;
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
