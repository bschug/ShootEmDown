using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class AutoCannonBullet : PlayerProjectileBase
{
    public override float Damage => damage;

    public float damage = 1;
    public float speed = 10;
    public float despawnHeight = -20;
    public IObjectPool<AutoCannonBullet> pool;

    private Rigidbody2D _rigidbody;

    public void Teleport(Vector2 position)
    {
        _rigidbody.position = position;
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var pos = _rigidbody.position;
        var y = pos.y - Time.deltaTime * speed;
        pos.Set(pos.x, y);
        _rigidbody.MovePosition(pos);
    }

    void Update()
    {
        if (_rigidbody.position.y < despawnHeight)
        {
            pool.Release(this);
        }
    }

    public override void OnHit(EnemyLifetime enemy)
    {
        pool.Release(this);
    }
}
