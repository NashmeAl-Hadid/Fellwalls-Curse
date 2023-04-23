using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ChaseState", menuName = "Unity-FSM/States/Chase", order = 3)]
public class ChaseState : AbstractFSMState
{ 
    public override void OnEnable()
    {
        base.OnEnable();
        stateType = FSMStateType.CHASE;
    }

    public override bool EnterState()
    {
        enteredState = false;

        if (base.EnterState())
        {
            enemy.enemyAnimator.SetBool("Run", true);
            enteredState = true;
        }

        return enteredState;
    }

    public override void UpdateState()
    {
        if (enteredState == true)
        {
            if (enemy.canMeleeAttack == true && enemy.MeleeAttackCheck())
            {
                if(enemy.meleeAttackTimer<=0)
                {
                    finiteStateMachine.EnterStateType(FSMStateType.MELEEATTACK);
                }    
                else
                {
                    finiteStateMachine.EnterStateType(FSMStateType.IDLE);
                }
            }
            else if (enemy.canRangeAttack == true && enemy.RangeAttackCheck())
            {
                if (enemy.rangeAttackTimer <= 0)
                {
                    finiteStateMachine.EnterStateType(FSMStateType.RANGEATTACK);
                }
            }
            else if (enemy.chase == true)
            {
                enemy.Chasing();
            }
            else
            {
                if(finiteStateMachine.startIdle==true)
                {
                    finiteStateMachine.EnterStateType(FSMStateType.IDLE);
                }
                else
                {
                    finiteStateMachine.EnterStateType(FSMStateType.PATROL);
                }
            }
        }
    }

    public override void ExitState()
    {
        enemy.enemyAnimator.SetBool("Run", false);
        base.ExitState();
    }

}
