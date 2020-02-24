using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var controller = animator.GetComponent<PlayerMovementController>();
        if (controller != null)
        {
            controller.StartAttack();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var controller = animator.GetComponent<PlayerMovementController>();
        if (controller != null)
        {
            controller.EndAttack();
        }
    }
}
