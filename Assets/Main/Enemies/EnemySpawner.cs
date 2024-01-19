using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private float _startTime;

    public float initialSpawnDelay = 5;
    public float minSpawnDelay = 0.1f;
    public float minSpawnDelayReachedAfter = 300;

    public EnemyPool enemyPool;
    public Transform leftmostSpawnPoint;
    public Transform rightmostSpawnPoint;

    public Vector3 _leftmostSpawnPoint;
    public Vector3 _rightmostSpawnPoint;


    private void Start()
    {
        _startTime = Time.time;
        _leftmostSpawnPoint = leftmostSpawnPoint.position;
        _rightmostSpawnPoint = rightmostSpawnPoint.position;

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            var delay = TimeUntilNextSpawn();
            Debug.LogFormat("Spawn next enemy in {0:F2} seconds", delay);
            yield return new WaitForSeconds(delay);

            SpawnEnemy();
        }
    }

    float TimeUntilNextSpawn()
    {
        var timePassed = Time.time - _startTime;

        if (timePassed >= minSpawnDelayReachedAfter)
        {
            return minSpawnDelay;
        }

        var t = timePassed / minSpawnDelayReachedAfter;
        return Mathf.Lerp(initialSpawnDelay, minSpawnDelay, t);
    }

    void SpawnEnemy()
    {
        var spawnPosition = Vector3.Lerp(_leftmostSpawnPoint, _rightmostSpawnPoint, Random.value);

        if (enemyPool.TryTakeEnemy(out var enemy))
        {
            enemy.Teleport(spawnPosition);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_leftmostSpawnPoint, _rightmostSpawnPoint);
        Gizmos.DrawSphere(leftmostSpawnPoint.position, 0.1f);
        Gizmos.DrawSphere(rightmostSpawnPoint.position, 0.1f);
    }
}
