using UnityEngine;

public abstract class PlayerProjectileBase : MonoBehaviour
{
    public abstract float Damage { get; }
    public abstract void OnHit(EnemyLifetime enemy);
}
