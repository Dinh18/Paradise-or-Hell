using UnityEngine;

public class ResetHitBox : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SkillHitBox skillHitBox = animator.gameObject.GetComponent<SkillHitBox>();
        if(skillHitBox != null) 
        {
            skillHitBox.DeactiveHitBox();
        }
    }
}
