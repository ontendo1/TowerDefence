using System.Linq;
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

        //Get target enemy; is first enemy in range.
        Vector3 targetEnemyPosition = enemiesInRange.First().position;

        //Calculate target enemy's position:
        float targetPosX = targetEnemyPosition.x - transform.position.x;
        float targetPosZ = targetEnemyPosition.z - transform.position.z;

        Vector3 direction = new Vector3(targetPosX, 0, targetPosZ).normalized;

        float angleInDegrees = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        //Throw arrow and turn to enemy.
        GameObject throwedBow = Instantiate(projectilePrfb, projectileSpawnPoint.position, Quaternion.Euler(Vector3.up * angleInDegrees));

        transform.rotation = Quaternion.Euler(Vector3.up * angleInDegrees);

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
