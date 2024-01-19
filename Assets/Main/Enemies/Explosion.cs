using System;
using UnityEngine;
using UnityEngine.Pool;

public class Explosion : MonoBehaviour
{
    public IObjectPool<Explosion> pool;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.keepAnimatorStateOnDisable = false;
    }
}
