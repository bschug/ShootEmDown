using UnityEngine;
using UnityEngine.Pool;

public class XpBubble : MonoBehaviour
{
    public int xp = 1;
    public float speed = 0.1f;
    public float cutoffHeight = 5f;

    private IObjectPool<XpBubble> _pool;

    public void SetPool(IObjectPool<XpBubble> pool)
    {
        _pool = pool;
    }

    public void Release()
    {
        _pool.Release(this);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.Translate(0, speed * Time.fixedDeltaTime, 0);

        if (transform.position.y > cutoffHeight)
        {
            Release();
        }
    }
}
