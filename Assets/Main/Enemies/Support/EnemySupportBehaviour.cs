using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySupportBehaviour : EnemyBehaviourBase
{
    public float speed = 1f;
    public float damage = 1f;
    public override float Damage => damage;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var newPosition = transform.position + Vector3.up * (Time.fixedDeltaTime * speed);
        _rigidbody.MovePosition(newPosition);
    }
}
