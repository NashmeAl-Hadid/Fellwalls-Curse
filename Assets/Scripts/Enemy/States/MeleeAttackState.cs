using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "MeleeAttackState", menuName = "Unity-FSM/States/MeleeAttack", order = 4)]
public class MeleeAttackState : AbstractFSMState
{
    //float totalDuration;

    public override void OnEnable()
    {
        base.OnEnable();
        stateType = FSMStateType.MELEEATTACK;
    }

    public override bool EnterState()
    {
        enteredState = false;

        if (base.EnterState())
        {
            //totalDuration = 0;
            enemy.isAttacking = true;
            enemy.enemyAnimator.SetBool("MeleeAttack", true);
            Debug.Log("Enter Attack State");
            enteredState = true;
        }

        return enteredState;
    }

    public override void UpdateState()
    {
        if (enteredState == true)
        {
            //totalDuration += Time.deltaTime;

            if (enemy.isAttacking==false)
            {
                finiteStateMachine.EnterStateType(FSMStateType.IDLE);
            }
        }
    }

    public override void ExitState()
    {
        enemy.enemyAnimator.SetBool("MeleeAttack", false);
        base.ExitState();
    }
}
