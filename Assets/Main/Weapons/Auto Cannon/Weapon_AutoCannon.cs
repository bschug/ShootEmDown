using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon_AutoCannon : MonoBehaviour
{
    public float speedFactor = 1f;
    public float secondsBetweenShots = 1f;
    public float windupTime = 0.1f;
    public bool debugChecksForPool = true;
    public int maxProjectiles = 50;
    public AutoCannonBullet projectilePrefab;

    public Transform leftSpawnPosition;
    public Transform rightSpawnPosition;

    private Animator _animator;
    private IObjectPool<AutoCannonBullet> _projectilePool;

    private static readonly int FireTriggerId = Animator.StringToHash("Fire");

    void Start()
    {
        _animator = GetComponent<Animator>();
        _projectilePool = new ObjectPool<AutoCannonBullet>(CreateBullet, OnTakeFromPool, OnReturnedToPool,
            OnDestroyBullet, debugChecksForPool, maxProjectiles, maxProjectiles);

        _animator.speed = speedFactor;
    }

    public void SetSpeed(float speedFactor)
    {
        this.speedFactor = speedFactor;
        _animator.speed = speedFactor;
    }

    private void OnEnable()
    {
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        while (gameObject.activeSelf)
        {
            if (_animator is null)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }

            if (Math.Abs(_animator.speed - speedFactor) > 0.001f)
            {
                _animator.speed = speedFactor;
            }

            yield return new WaitForSeconds(secondsBetweenShots / speedFactor);
            _animator.SetTrigger(FireTriggerId);

            yield return new WaitForSeconds(windupTime / speedFactor);

            SpawnBulletAt(leftSpawnPosition.position);
            SpawnBulletAt(rightSpawnPosition.position);
        }
    }

    void SpawnBulletAt(Vector2 position)
    {
        var projectile = _projectilePool.Get();
        projectile.Teleport(position);
    }

    AutoCannonBullet CreateBullet()
    {
        var bullet = Instantiate(projectilePrefab);
        bullet.pool = _projectilePool;
        bullet.gameObject.SetActive(false);
        return bullet;
    }

    void OnTakeFromPool(AutoCannonBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    void OnReturnedToPool(AutoCannonBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    void OnDestroyBullet(AutoCannonBullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
