using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Weapons/Projectile", order = 0)]
public class ProjectileSpecSO : ScriptableObject
{
    public float damage;
    public float speed;
}
