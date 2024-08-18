using UnityEngine;

public class Catapult : Weapon
{
    public override void ShotProjectile()
    {
        base.ShotProjectile();

        GameObject throwedBall = objectPooler.SpawnFromPool("Catapult", projectileSpawnPoint.position, Quaternion.Euler(Vector3.up * angleInDegrees), projectilePrfb);

        //Set direction and vertical force of catapult's heavy ball.
        if (throwedBall.TryGetComponent(out ParabolicProjectile projectile))
        {
            projectile.direction = direction;
            projectile.targetTransform = targetEnemy;
            projectile.OnThrowed();
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
