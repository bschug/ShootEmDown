using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public bool debugChecks = true;
    public int maxEnemies = 100;
    public int maxExplosions = 30;

    public EnemyLifetime enemyPrefab;
    public Explosion explosionPrefab;

    private IObjectPool<EnemyLifetime> _enemyPool;
    private IObjectPool<Explosion> _explosionPool;

    public bool TryTakeEnemy(out EnemyLifetime enemy)
    {
        enemy = _enemyPool.Get();
        return enemy is not null;
    }

    void Start()
    {
        _enemyPool = new ObjectPool<EnemyLifetime>(CreateEnemy, OnTakeEnemy, OnEnemyReturned, OnDestroyEnemy,
            debugChecks, maxEnemies, maxEnemies);
        _explosionPool = new ObjectPool<Explosion>(CreateExplosion, OnTakeExplosion, OnExplosionReturned,
            OnDestroyExplosion, debugChecks, maxExplosions, maxExplosions);
    }

    EnemyLifetime CreateEnemy()
    {
        var enemy = Instantiate(enemyPrefab);
        enemy.pool = _enemyPool;
        enemy.gameObject.SetActive(false);
        return enemy;
    }

    void OnTakeEnemy(EnemyLifetime enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.Reset();
    }

    void OnEnemyReturned(EnemyLifetime enemy)
    {
        enemy.gameObject.SetActive(false);

        if (enemy.CurrentHealth <= 0)
        {
            SpawnExplosion(enemy.transform.position);
        }
    }

    void SpawnExplosion(Vector3 position)
    {
        var explosion = _explosionPool.Get();
        if (explosion is not null)
        {
            explosion.transform.position = position;
        }
    }

    void OnDestroyEnemy(EnemyLifetime enemy)
    {
        Destroy(enemy.gameObject);
    }

    Explosion CreateExplosion()
    {
        var explosion = Instantiate(explosionPrefab);
        explosion.pool = _explosionPool;
        explosion.gameObject.SetActive(false);
        return explosion;
    }

    void OnTakeExplosion(Explosion explosion)
    {
        explosion.gameObject.SetActive(true);
    }

    void OnExplosionReturned(Explosion explosion)
    {
        explosion.gameObject.SetActive(false);
    }

    void OnDestroyExplosion(Explosion explosion)
    {
        Destroy(explosion.gameObject);
    }
}
