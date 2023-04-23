using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackThreeBehaviour : StateMachineBehaviour
{
    private PlayerCombat playerCombat;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerCombat = animator.GetComponent<PlayerCombat>();

        playerCombat.attackOne = false;
        playerCombat.attackTwo = false;
        playerCombat.attackThree = false;
    }

    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("RunToAttackBool", false);
        animator.ResetTrigger("Attack3");
    }

    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
