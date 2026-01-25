using UnityEngine;

public class ResetHitBoxBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInChildren<EnemyHitBox>().DeactiveHitBox();
    }
}
