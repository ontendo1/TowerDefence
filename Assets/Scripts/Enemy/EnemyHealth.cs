using PathCreation.Examples;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [HideInInspector] public float defaultHealth;

    private float _health;
    public float Health
    {
        get => _health;

        set
        {
            _health = value;
            healthBar.fillAmount = _health / defaultHealth;

            if (_health <= 0)
            {
                ObjectPoolManager.Instance.AddToPool("Enemy", gameObject);

                if (gameObject.TryGetComponent(out PathFollower pathFollower))
                {
                    Destroy(pathFollower);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            if (other.TryGetComponent(out Projectile projectile))
            {
                ObjectPoolManager.Instance.AddToPool(projectile.objPoolName, other.gameObject);
                Health -= projectile.projectileSpec.damage;
            }
        }
    }
}
