
using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    private float _damageTaken = 0;
    public float CurrentHealth => maxHealth - _damageTaken;

    public int iframesAfterHit = 5;
    private int _iframesRemaining = 0;

    public int currentXp = 0;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out EnemyBehaviourBase enemy))
        {
            TakeDamage(enemy.Damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out XpBubble xpBubble))
        {
            GainXp(xpBubble.xp);
            xpBubble.Release();
        }
    }

    private void TakeDamage(float damage)
    {
        if (_iframesRemaining > 0)
        {
            return;
        }

        _damageTaken += damage;
        _iframesRemaining = Mathf.Max(_iframesRemaining, iframesAfterHit);
    }

    private void GainXp(int xp)
    {
        currentXp += xp;
    }

    private void FixedUpdate()
    {
        if (_iframesRemaining > 0)
        {
            _iframesRemaining -= 1;
        }
    }
}
