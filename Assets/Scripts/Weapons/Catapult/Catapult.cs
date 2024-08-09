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

        GameObject throwedBall = objectPooler.SpawnFromPool("Catapult", projectileSpawnPoint.position, Quaternion.Euler(Vector3.up * angleInDegrees), projectilePrfb);

        //Set direction and vertical force of catapult's heavy ball.
        if (throwedBall.TryGetComponent(out ParabolicProjectile projectile))
        {
            projectile.direction = direction;
            projectile.targetTransform = targetEnemy;
        }

        //Calls methods for reset object status in previous use. Makes the object brand new.
        if (throwedBall.TryGetComponent(out IPoolableObject poolableObject))
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
