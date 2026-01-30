using UnityEngine;

public class ResetHitBoxBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetComponentInChildren<EnemyHitBox>() != null) animator.GetComponentInChildren<EnemyHitBox>().DeactiveHitBox();
    }
}
