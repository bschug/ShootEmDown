using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public bool debugChecks = true;
    public int maxEnemies = 100;
    public int maxExplosions = 30;

    public EnemyLifetime enemyPrefab;
    public Explosion explosionPrefab;
    public XpBubble xpBubblePrefab;

    private IObjectPool<EnemyLifetime> _enemyPool;
    private IObjectPool<Explosion> _explosionPool;
    private IObjectPool<XpBubble> _xpBubblePool;

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
        _xpBubblePool = new ObjectPool<XpBubble>(CreateXpBubble, null, null, null, debugChecks, 200, 1000);
    }

    XpBubble CreateXpBubble()
    {
        var xpBubble = Instantiate(xpBubblePrefab);
        xpBubble.SetPool(_xpBubblePool);
        xpBubble.gameObject.SetActive(false);
        return xpBubble;
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
            var position = enemy.transform.position;
            SpawnExplosion(position);
            SpawnXpBubble(position);
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

    void SpawnXpBubble(Vector3 position)
    {
        var xpBubble = _xpBubblePool.Get();
        if (xpBubble is not null)
        {
            xpBubble.transform.position = position;
            xpBubble.gameObject.SetActive(true);
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
