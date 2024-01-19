using UnityEngine;

public class ReturnExplosionToPoolOnStateExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var explosion = animator.GetComponent<Explosion>();
		explosion.pool.Release(explosion);
    }
}
