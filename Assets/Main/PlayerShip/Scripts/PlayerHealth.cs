
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float _damageTaken = 0;
    public float CurrentHealth => maxHealth - _damageTaken;

    public int iframesAfterHit = 5;
    private int _iframesRemaining = 0;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_iframesRemaining > 0)
        {
            return;
        }

        if (other.collider.TryGetComponent(out EnemyBehaviourBase enemy))
        {
            _damageTaken += enemy.Damage;
            _iframesRemaining = Mathf.Max(_iframesRemaining, iframesAfterHit);
        }
    }

    private void FixedUpdate()
    {
        if (_iframesRemaining > 0)
        {
            _iframesRemaining -= 1;
        }
    }
}
