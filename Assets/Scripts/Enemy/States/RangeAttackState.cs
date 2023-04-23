using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "RangeAttackState", menuName = "Unity-FSM/States/RangeAttack", order = 5)]
public class RangeAttackState : AbstractFSMState
{

    public override void OnEnable()
    {
        base.OnEnable();
        stateType = FSMStateType.RANGEATTACK;
    }

    public override bool EnterState()
    {
        enteredState = false;

        if (base.EnterState())
        {
            if(enemy.targetInBack==true)
            {
                enemy.Flip();
                enemy.targetInBack = false;
            }
            if(enemy.isAttackCancelable == false)
            {
                enemy.isAttacking = true;
            }
                
            enemy.enemyAnimator.SetBool("RangeAttack", true);
            Debug.Log("Enter Attack State");
            enteredState = true;
        }

        return enteredState;
    }

    public override void UpdateState()
    {
        if (enteredState == true)
        {

            if (enemy.isAttackCancelable == true && enemy.RangeAttackCheck() == false)
            {
                finiteStateMachine.EnterStateType(FSMStateType.IDLE);
            }
            if(enemy.isAttackCancelable == false && enemy.isAttacking == false)
            {
                finiteStateMachine.EnterStateType(FSMStateType.IDLE);
            }

        }
    }

    public override void ExitState()
    {
        enemy.enemyAnimator.SetBool("RangeAttack", false);
        base.ExitState();
    }
}
