using UnityEngine;
using UnityEngine.Pool;

public class EnemyLifetime : MonoBehaviour
{
    public float maxHealth = 10;
    private float _damageTaken = 0;
    public float CurrentHealth => maxHealth - _damageTaken;

    public float despawnHeight = 5;

    public IObjectPool<EnemyLifetime> pool;

    private Rigidbody2D _rigidbody;

    public void Reset()
    {
        _damageTaken = 0;
    }

    public void Teleport(Vector2 position)
    {
        var wasKinematicBefore = _rigidbody.isKinematic;
        _rigidbody.isKinematic = true;
        transform.position = position;
        transform.rotation = Quaternion.identity;
        _rigidbody.isKinematic = wasKinematicBefore;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0;
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_rigidbody.position.y > despawnHeight)
        {
            pool.Release(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out PlayerProjectileBase projectile))
        {
            TakeDamage(projectile.Damage);
            projectile.OnHit(this);
        }
    }

    private void TakeDamage(float damage)
    {
        _damageTaken += damage;

        if (_damageTaken >= maxHealth)
        {
            pool.Release(this);
        }
    }
}
